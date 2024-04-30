using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TktProject.Infrastructure.Contracts;
using TktProject.Infrastructure.Repositories;
using TktProject.Persistance.DbContexts;

namespace TktProject.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceService(IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<TktDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("TktDatabase")));

        services.AddScoped<IDbContext, TktDbContext>();
        services.AddScoped<ITktDbContext, TktDbContext>();

        services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
        services.AddScoped(typeof(IUnitOfWork<ITktDbContext>), typeof(UnitOfWork<ITktDbContext>));
        return services;
    }
}
