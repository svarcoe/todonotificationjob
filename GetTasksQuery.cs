using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Paramore.Darker;
using Paramore.Darker.Policies;
using Paramore.Darker.QueryLogging;

namespace TodoScheduledJob
{
    public sealed class GetTasksQuery : IQuery<IReadOnlyDictionary<int, TodoModel>>
    {

    }

    public sealed class GetTasksQueryHandler : QueryHandlerAsync<GetTasksQuery, IReadOnlyDictionary<int, TodoModel>>
    {
        [QueryLogging(1)]
        [RetryableQuery(2)]
        public override async Task<IReadOnlyDictionary<int, TodoModel>> ExecuteAsync(GetTasksQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var repository = new TodoRepository();
            return await repository.GetAll(cancellationToken);
        }
    }
}