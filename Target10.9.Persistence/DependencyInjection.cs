using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Repositories;
using Target10._9.Persistencence.Repositories;

namespace Target10._9.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(Constants.ConnectionString);
            options.UseSqlServer(connectionString);
        });
        
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}