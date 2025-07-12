using Microsoft.OpenApi.Models;

namespace Target10._9_api.Configuration;

public static class SwaggerIntegration
{
    public static void AddSwaggerOptions(this IServiceCollection services)
    {
        var bearerScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Entrez 'Bearer' [espace] et ensuite votre token JWT.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        };

        var requirement = new OpenApiSecurityRequirement
        {
            { bearerScheme, Array.Empty<string>() }
        };

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Mon API",
                Version = "v1",
                Description = "API d'exemple pour démonstration"
            });

            c.AddSecurityDefinition("Bearer", bearerScheme);
            c.AddSecurityRequirement(requirement);
            c.EnableAnnotations();
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }
}