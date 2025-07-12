using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Accounts.Mappings;

namespace Target10._9.Business.Accounts
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddAccountsDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAccountsService, AccountsService>();
            //Mappings
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
