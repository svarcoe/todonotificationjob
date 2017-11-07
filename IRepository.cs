using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IRepository<T>
{
    Task<IReadOnlyDictionary<int, T>> GetAll(CancellationToken cancellationToken);
    T GetById(string id);
    T Update (T model);
    T Create(T model);
    bool Delete(T model);
}