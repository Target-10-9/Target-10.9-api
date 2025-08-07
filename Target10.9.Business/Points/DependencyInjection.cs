using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.Points
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddPointsDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPointsService, PointsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
