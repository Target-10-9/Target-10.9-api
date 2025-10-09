using System.Text;
using Amazon.Lambda.AspNetCoreServer.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Target10._9_api;
using Target10._9_api.Configurations;
using Target10._9_api.Middlewares;
using Target10._9.Business;
using Target10._9.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

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
            policy.WithOrigins(
                    "http://localhost:5173",
                    "http://15.237.25.38"
                )
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

var conn = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    // Si tes migrations sont dans Infrastructure :
    opt.UseNpgsql(conn, x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
    //opt.UseNpgsql(conn);
});

//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerOptions();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceDependencies(builder.Configuration);
builder.Services.AddBusinessDependencies();

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//     var cfg    = scope.ServiceProvider.GetRequiredService<IConfiguration>();
//     var cs     = cfg.GetConnectionString("DefaultConnection");
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     
//     logger.LogInformation("▶️ Tentative de migration sur « {CS} »", cs);
//     try
//     {
//         db.Database.Migrate();
//         logger.LogInformation("✅ Migration réussie.");
//     }
//     catch (Exception ex)
//     {
//         logger.LogError(ex, "❌ Migration échouée !");
//         throw;
//     }
// }




app.UseMiddleware<ExceptionHandlingMiddleware>();

// if (app.Environment.IsDevelopment())
// {
//     
// }

app.UseSwagger(c =>
{
    // Si Lambda, on doit dire à Swagger de "préfixer" ses routes
    var basePath = Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") != null
        ? "/dev"
        : string.Empty;

    // Important pour que Swagger génère le bon chemin interne
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers = new List<Microsoft.OpenApi.Models.OpenApiServer>
        {
            new Microsoft.OpenApi.Models.OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}" }
        };
    });
});

app.UseSwaggerUI(c =>
{
    var basePath = Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") != null
        ? "/dev"
        : string.Empty;

    c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Target10.9 API v1");
    c.RoutePrefix = "swagger";
});





if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}
// app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Ok("Target10.9 API is running"));

app.Run();