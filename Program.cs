using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Paramore.Darker;
using Paramore.Darker.AspNetCore;
using Paramore.Darker.Builder;
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

            var registry = new QueryHandlerRegistry();
            registry.Register<GetTasksQuery, IReadOnlyDictionary<string, TodoModel>, GetTasksQueryHandler>();

            IQueryProcessor queryProcessor = serviceProvider.GetService<IQueryProcessor>();
            logger.LogDebug("Starting application");
            var tasks = queryProcessor.ExecuteAsync(new GetTasksQuery()).Result;
            foreach (var item in tasks)
            {
                Console.WriteLine($"{item.Value.Title}, assigned to: {item.Value.AssignedUserName}");
            }
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
            logger.LogDebug("Application ending.");            
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
