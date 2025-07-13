using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.ModeDetails
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddModeDetailsDependencies(this IServiceCollection services)
        {
            services.AddScoped<IModeDetailsService, ModeDetailsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
