using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Target10._9_api;
using Target10._9_api.Configurations;
using Target10._9_api.Middlewares;
using Target10._9.Business;
using Target10._9.Persistence;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAuthorization();

builder.Services.AddApiServices();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerOptions();

builder.Services.AddPersistenceDependencies(builder.Configuration);
builder.Services.AddBusinessDependencies();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var cfg    = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var cs     = cfg.GetConnectionString("DefaultConnection");
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    logger.LogInformation("▶️ Tentative de migration sur « {CS} »", cs);
    try
    {
        db.Database.Migrate();
        logger.LogInformation("✅ Migration réussie.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Migration échouée !");
        throw;
    }
}




app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Target10.9 API v1");
        c.RoutePrefix = "swagger";  // URL finale : /swagger/index.html
    });
}



app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();