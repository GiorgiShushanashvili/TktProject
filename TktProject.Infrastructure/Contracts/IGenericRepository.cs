using System.Linq.Expressions;

namespace TktProject.Infrastructure.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IQueryable<T>> GetAllAsync();
    Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IQueryable<T>> FindManyAsync(Expression<Func<T, bool>> predicate);
    ValueTask AddAsync(T entity);
    ValueTask AddRangeAsync(IList<T> entities);
    ValueTask UpdateAsync(T entity);
    ValueTask Remove(T entity);
    ValueTask RemoveRange(IList<T> entities);
    IQueryable<T> Table {  get; } 
}
