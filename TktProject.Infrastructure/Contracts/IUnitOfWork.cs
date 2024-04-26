namespace TktProject.Infrastructure.Contracts;

public interface IUnitOfWork<TContext>:IDisposable
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    int Save();
    Task<int> SaveAsync(CancellationToken cancellationToken=default);
}
