using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Users.Mappings;

namespace Target10._9.Business.Users
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddUsersDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
