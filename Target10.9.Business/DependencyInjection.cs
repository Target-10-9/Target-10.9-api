using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Authentications;
using Target10._9.Business.Logs;
using Target10._9.Business.ModeDetails;
using Target10._9.Business.Points;
using Target10._9.Business.Services;
using Target10._9.Business.SessionModes;
using Target10._9.Business.SessionModeWeaponDetails;
using Target10._9.Business.Sessions;
using Target10._9.Business.Users;
using Target10._9.Business.WeaponDetails;

namespace Target10._9.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessDependencies(this IServiceCollection services)
    {
        var businessAssembly = typeof(DependencyInjection).GetTypeInfo().Assembly;

        return services
            .AddAuthenticationsDependencies()
            .AddUsersDependencies()
            .AddWeaponDetailsDependencies()
            .AddLogsDependencies()
            .AddSessionsDependencies()
            .AddSessionModesDependencies()
            .AddModeDetailsDependencies()
            .AddSessionModeWeaponDetailsDependencies()
            .AddPointsDependencies()
            .AddScoped<IValidationService, ValidationService>()
            .AddValidatorsFromAssembly(businessAssembly);
    }
}