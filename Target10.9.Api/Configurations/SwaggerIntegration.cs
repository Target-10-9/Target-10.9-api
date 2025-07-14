using Microsoft.OpenApi.Models;

namespace Target10._9_api.Configurations;

public static class SwaggerIntegration
{
    public static void AddSwaggerOptions(this IServiceCollection services)
    {
        var bearerScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter 'Bearer' [space] and then your JWT token.",
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
                Title = "My API",
                Version = "v1",
                Description = "Sample API for demonstration"
            });

            c.AddSecurityDefinition("Bearer", bearerScheme);
            c.AddSecurityRequirement(requirement);
            c.EnableAnnotations();
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }
}