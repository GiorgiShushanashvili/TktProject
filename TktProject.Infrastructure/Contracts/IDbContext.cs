using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TktProject.Infrastructure.Contracts;

public interface IDbContext:IDisposable
{
    DbSet<T> Set<T>() where T : class;
    EntityEntry<T> Entry<T>(T entity) where T : class;
    int Save();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
}
