using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.SessionModes
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddSessionModesDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISessionModesService, SessionModesService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
