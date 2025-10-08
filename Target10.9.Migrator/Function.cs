// Target10.9.Target10.9.Migrator/Function.cs
using Amazon.Lambda.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Target10._9.Persistence;

// Requis par Lambda pour la (dé)sérialisation
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Target10._9.Migrator;

public class Function
{
    private readonly IHost _host;

    public Function()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((ctx, services) =>
            {
                var conn = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                services.AddDbContext<ApplicationDbContext>(opt =>
                {
                    // Si tes migrations sont dans Infrastructure :
                    // opt.UseNpgsql(conn, x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                    opt.UseNpgsql(conn);
                });
                // Ajoute ici tes registrations Business/Infra si nécessaires
            })
            .Build();
    }

    // Handler Lambda sans entrée particulière
    public async Task<object> FunctionHandler(object input, ILambdaContext context)
    {
        using var scope = _host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();
        return new { status = "ok" };
    }
}