using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.Logs
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddLogsDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILogsService, LogsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
