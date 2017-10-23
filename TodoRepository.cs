using System.Collections.Generic;

public class TodoRepository : ITodoRepository {

    public TodoModel Create(TodoModel model)
    {
        throw new System.NotImplementedException();
    }

    public bool Delete(TodoModel model)
    {
        throw new System.NotImplementedException();
    }

    public List<TodoModel> GetAll()
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
}

public interface ITodoRepository : IRepository<TodoModel>{
    
}