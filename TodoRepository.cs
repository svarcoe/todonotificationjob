using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class TodoRepository : ITodoRepository {

    public TodoModel Create(TodoModel model)
    {
        throw new System.NotImplementedException();
    }

    public bool Delete(TodoModel model)
    {
        throw new System.NotImplementedException();
    }

    public TodoModel GetById(string id)
    {
        throw new System.NotImplementedException();
    }

    public TodoModel Update(TodoModel model)
    {
        throw new System.NotImplementedException();
    }

    public Task<IReadOnlyDictionary<int, TodoModel>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public interface ITodoRepository : IRepository<TodoModel>{
    
}