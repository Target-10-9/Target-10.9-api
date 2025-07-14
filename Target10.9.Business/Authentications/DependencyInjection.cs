using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.Authentications
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddAuthenticationsDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationsService, AuthenticationsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
