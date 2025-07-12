using Target10._9_api.Providers;
using Target10._9.Business.Providers;

namespace Target10._9_api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }
}