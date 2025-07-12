using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Target10._9.Business.Accounts;
using Target10._9.Business.Services;

namespace Target10._9.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessDependencies(this IServiceCollection services)
    {
        var businessAssembly = typeof(DependencyInjection).GetTypeInfo().Assembly;

        return services
            .AddAccountsDependencies()
            .AddScoped<IValidationService, ValidationService>()
            .AddValidatorsFromAssembly(businessAssembly);
    }
}