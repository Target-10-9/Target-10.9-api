using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Logs.Repositories;
using Target10._9.Business.ModeDetails.Repositories;
using Target10._9.Business.SessionModes.Repositories;
using Target10._9.Business.SessionModeWeaponDetails.Repositories;
using Target10._9.Business.Sessions.Repositories;
using Target10._9.Business.Users.Repositories;
using Target10._9.Business.WeaponDetails.Repositories;
using Target10._9.Persistence.Repositories.Logs;
using Target10._9.Persistence.Repositories.ModeDetails;
using Target10._9.Persistence.Repositories.SessionModes;
using Target10._9.Persistence.Repositories.SessionModeWeaponDetails;
using Target10._9.Persistence.Repositories.Sessions;
using Target10._9.Persistence.Repositories.Users;
using Target10._9.Persistence.Repositories.WeaponDetails;

namespace Target10._9.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IDbConfiguration, DefaultDbConfiguration>();
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ILogsRepository, LogsRepository>();
        services.AddScoped<IModeDetailsRepository, ModeDetailsRepository>();
        services.AddScoped<ISessionModesRepository, SessionModesRepository>();
        services.AddScoped<ISessionModeWeaponDetailsRepository, SessionModeWeaponDetailsRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<IWeaponDetailsRepository, WeaponDetailsRepository>();

        return services;
    }
}