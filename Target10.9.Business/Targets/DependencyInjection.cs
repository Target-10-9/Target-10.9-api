using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.Targets
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddTargetsDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITargetsService, TargetsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
