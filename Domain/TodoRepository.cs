using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class TodoRepository : ITodoRepository 
{
    private static readonly IReadOnlyDictionary<string, TodoModel> Db = new Dictionary<string, TodoModel>
    {
        { "1", new TodoModel()
            { 
                Id = "1",
                AssignedUserName = "Scott Varcoe",
                AssignedUserEmail = "svarcoe@gmail.com",
                DueDate = DateTime.Today,
                Title = "Test Todo 1"
            } 
        },
        { "2", new TodoModel()
            { 
                Id = "2",
                AssignedUserName = "Scott Varcoe",
                AssignedUserEmail = "svarcoe@gmail.com",
                DueDate = DateTime.Today,
                Title = "Test Todo 2"
            } 
        },
        { "3", new TodoModel()
            { 
                Id = "3",
                AssignedUserName = "Scott Varcoe",
                AssignedUserEmail = "svarcoe@gmail.com",
                DueDate = DateTime.Today,
                Title = "Test Todo 3"
            } 
        },
        { "4", new TodoModel()
            { 
                Id = "4",
                AssignedUserName = "Scott Varcoe",
                AssignedUserEmail = "svarcoe@gmail.com",
                DueDate = DateTime.Today,
                Title = "Test Todo 4"
            } 
        },
        
    };

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

    public async Task<IReadOnlyDictionary<string, TodoModel>> GetAll(CancellationToken cancellationToken)
    {
        return await Task.FromResult(Db);
    }
}

public interface ITodoRepository : IRepository<TodoModel>{
    
}