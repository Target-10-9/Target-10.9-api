using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Target10._9.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessDependencies(this IServiceCollection services)
    {
        Assembly businessAssembly = typeof(DependencyInjection).GetTypeInfo().Assembly;

        // services.AddScoped<IUserService, UserService>();
        return services;
    }
}