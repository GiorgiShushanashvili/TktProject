using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TktProject.Domain.Entities;
using TktProject.Infrastructure.Contracts;

namespace TktProject.Persistance.DbContexts;

public class TktDbContext:DbContext,ITktDbContext
{
    public TktDbContext(DbContextOptions<TktDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Tickets> Tickets { get; set; }

    public int Save()
    {
        return base.SaveChanges();
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
