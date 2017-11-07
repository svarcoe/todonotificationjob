using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Paramore.Darker.AspNetCore;
using Paramore.Darker.Policies;
using Paramore.Darker.QueryLogging;
using Polly;

namespace TodoScheduledJob
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static void Main(string[] args)
        {
            
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = configurationBuilder.Build();

            //setup our DI
            IServiceCollection services = new ServiceCollection()
                .AddLogging()
                .AddSingleton(new LoggerFactory().AddConsole())
                .AddSingleton<ITodoRepository, TodoRepository>();
            
            services
                .AddDarker()
                .AddHandlersFromAssemblies(typeof(Program).Assembly)
                .AddJsonQueryLogging()
                .AddPolicies(ConfigurePolicies());
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            ILogger logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");
            
            logger.LogDebug("Application ending.");
            Thread.Sleep(1000);
        }

        private static IPolicyRegistry ConfigurePolicies()
        {
            var defaultRetryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromMilliseconds(50),
                    TimeSpan.FromMilliseconds(100),
                    TimeSpan.FromMilliseconds(150)
                });

            var circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(1, TimeSpan.FromMilliseconds(500));

            return new PolicyRegistry
            {
                { Paramore.Darker.Policies.Constants.RetryPolicyName, defaultRetryPolicy },
                { Paramore.Darker.Policies.Constants.CircuitBreakerPolicyName, circuitBreakerPolicy }
            };
        }
    }
}
