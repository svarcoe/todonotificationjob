using System.Collections.Generic;

public interface IRepository<T>
{
    List<T> GetAll();
    T GetById(string id);
    T Update (T model);
    T Create(T model);
    bool Delete(T model);
}