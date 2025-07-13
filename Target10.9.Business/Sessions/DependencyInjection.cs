using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.Sessions
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddSessionsDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISessionsService, SessionsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
