using Microsoft.EntityFrameworkCore;
using TktProject.Domain.Entities;

namespace TktProject.Infrastructure.Contracts;

public interface ITktDbContext:IDbContext
{
    DbSet<User> Users=>Set<User>();
    DbSet<Tickets> Tickets=>Set<Tickets>();
    DbSet<Order> Orders=>Set<Order>();
    DbSet<UserProfile> UserProfile=>Set<UserProfile>();
}
