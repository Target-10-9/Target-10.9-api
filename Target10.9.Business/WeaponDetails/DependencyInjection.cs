using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.WeaponDetails
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddWeaponDetailsDependencies(this IServiceCollection services)
        {
            services.AddScoped<IWeaponDetailsService, WeaponDetailsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
