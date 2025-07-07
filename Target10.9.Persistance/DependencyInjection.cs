using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Target10._9.Persistance;

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

        return services;
    }
}