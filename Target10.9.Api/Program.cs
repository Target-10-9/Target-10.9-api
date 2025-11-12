using System.Text;
using Amazon.Lambda.AspNetCoreServer.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Target10._9_api;
using Target10._9_api.Configurations;
using Target10._9_api.Middlewares;
using Target10._9.Business;
using Target10._9.Persistence;

var builder = WebApplication.CreateBuilder(args);
 
// Logger global pour debug
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger("DB_DEBUG");

 bool runningOnLambda = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME"));

 if (runningOnLambda)
 {
     builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
 }



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
logger.LogInformation("▶️ DB_CONNECTION_STRING utilisée : {Conn}", conn);

try
{
    logger.LogInformation("🔹 Tentative de connexion brute Npgsql...");
    using var testConn = new NpgsqlConnection(conn);
    testConn.Open();
    logger.LogInformation("✅ Connexion Npgsql brute réussie !");
    testConn.Close();
}
catch (Exception ex)
{
    logger.LogError(ex, "❌ Échec de la connexion Npgsql brute ! Détails complets : {Message}", ex.ToString());
}


// builder.Services.AddDbContext<ApplicationDbContext>(opt =>
// {
//     // Si tes migrations sont dans Infrastructure :
// // opt.UseNpgsql(conn, x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
// // opt.UseNpgsql(conn)
// //     .EnableSensitiveDataLogging()
// //     .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
//
//     opt.UseNpgsql(conn, x =>
//             x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
//         .EnableSensitiveDataLogging()
//         .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
//     
//     //opt.UseNpgsql(conn);
// });

logger.LogInformation("▶️ DB_CONNECTION_STRING utilisée : {Conn}", conn);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(conn, x =>
            x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
});



//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerOptions();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceDependencies(builder.Configuration);
builder.Services.AddBusinessDependencies();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        logger.LogInformation("🔹 Tentative de db.Database.CanConnect()...");
        if (db.Database.CanConnect())
        {
            logger.LogInformation("✅ EF Core peut se connecter à la base de données !");
        }
        else
        {
            
            logger.LogWarning("⚠️ EF Core ne peut pas se connecter à la base de données ! " + db.Logs );
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Exception EF Core lors de CanConnect() : {Message}", ex.ToString());
        throw new Exception("❌ Impossible de se connecter à la base de données via EF Core !", ex);
    }
}

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
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        var stage = Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") != null ? "/dev" : "";
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{stage}" }
        };
    });
});

// ⚠️ Correction ici :
app.UseSwaggerUI(c =>
{
    // Récupère dynamiquement le stage depuis la requête (plus fiable que les variables d'environnement)
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/dev/swagger/v1/swagger.json", "Target10.9 API v1");
});

// app.MapGet("/env", () =>
// {
//     var isLambda = Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") != null;
//     return Results.Ok(new
//     {
//         RunningOnLambda = isLambda,
//         AWS_LAMBDA_FUNCTION_NAME = Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")
//     });
// });



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