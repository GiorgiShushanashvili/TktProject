using TktProject.Infrastructure.Contracts;

namespace TktProject.Infrastructure.Repositories;

public class UnitOfWork<TContext>:IUnitOfWork<TContext> where TContext : IDbContext
{
    private readonly TContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private bool _disposed;
    public UnitOfWork(TContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        _disposed = false; 
    }
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
        }
        var repository = new GenericRepository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }
    public int Save()
    {
        return _context.Save();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
