using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.SessionModeWeaponDetails
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddSessionModeWeaponDetailsDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISessionModeWeaponDetailsService, SessionModeWeaponDetailsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
