using System.Linq.Expressions;
using Catalog.Service.Domain;

namespace Catalog.Service.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T?> GetAsync(Guid id);
    // Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task UpdateAsync(T item);
    Task CreateAsync(T item);
    Task RemoveAsync(Guid id);
}