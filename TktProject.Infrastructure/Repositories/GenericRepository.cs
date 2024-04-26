using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TktProject.Infrastructure.Contracts;

namespace TktProject.Infrastructure.Repositories;

public class GenericRepository<T>:IGenericRepository<T> where T : class
{
    private readonly IDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(IDbContext context)
    {
        _context = context;
        _dbSet=context.Set<T>();
    }

    public IQueryable<T> Table => _dbSet;

    public virtual async Task<IQueryable<T?>> GetAllAsync()
    {
        return await Task.FromResult(_dbSet.AsNoTracking());
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IQueryable<T?>> FindManyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.FromResult(_dbSet.Where(predicate).AsNoTracking());
    }

    public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async ValueTask AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async ValueTask AddRangeAsync(IList<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual ValueTask UpdateAsync(T entity)
    {
        if (entity == null) throw new ArgumentException("Something Is Wrong");
        _dbSet.Entry(entity).State = EntityState.Modified;
        _dbSet.Entry(entity).CurrentValues.SetValues(entity);
        return ValueTask.CompletedTask;
    }

    public virtual ValueTask Remove(T entity)
    {
        _dbSet.Remove(entity);
        return ValueTask.CompletedTask;
    }

    public virtual ValueTask RemoveRange(IList<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return ValueTask.CompletedTask;
    }
}