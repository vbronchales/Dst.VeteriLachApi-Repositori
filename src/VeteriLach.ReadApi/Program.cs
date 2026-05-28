using FluentValidation;
using Serilog;
using VeteriLach.ReadApi.Middleware;
using VeteriLach.ReadApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===== Afegir appsettings.Local.json per a configuració local amb secrets =====
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// ===== Configurar Serilog =====
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "VeteriLach.ReadApi")
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/veterilach-api-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Iniciant VeteriLach.ReadApi");

    // ===== Add services to the container =====
    builder.Services.AddControllers(options =>
        {
            // Afegir filtre global per validacions
            options.Filters.Add<VeteriLach.ReadApi.Middleware.ValidationExceptionFilter>();
        })
        .AddJsonOptions(options =>
        {
            // Configurar serialització JSON
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

    // Configurar CORS (opcional, per si s'accedeix des de frontend)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    // Configurar Swagger/OpenAPI
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "VeteriLach Read API",
            Version = "v1",
            Description = "API REST de Consulta per a Integració MCP amb IA - Només Lectura",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "VeteriLach Team",
                Email = "info@veterilach.com"
            }
        });

        // Afegir suport per a API Key al Swagger UI
        options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Name = "X-API-Key",
            Description = "API Key per autenticar-se. Contacti amb l'administrador per obtenir una clau."
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    // ===== Configurar Entity Framework Core =====
    builder.Services.AddDbContext<VeteriLachDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("VeteriLachDb"));
        // Només lectura: deshabilitar tracking per millorar rendiment
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

    // ===== Configurar MediatR =====
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        // Registrar pipeline behaviors (ordre important: validació → logging)
        cfg.AddOpenBehavior(typeof(VeteriLach.ReadApi.Application.Common.Behaviors.ValidationBehavior<,>));
        cfg.AddOpenBehavior(typeof(VeteriLach.ReadApi.Application.Common.Behaviors.LoggingBehavior<,>));
    });

    // ===== Configurar AutoMapper =====
    builder.Services.AddAutoMapper(typeof(Program).Assembly);

    // ===== Configurar FluentValidation =====
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    // ===== Registrar serveis d'aplicació =====
    builder.Services.AddScoped<VeteriLach.ReadApi.Application.MedicalHistory.Services.TextVisitaParserService>();

    var app = builder.Build();

    // ===== Configure the HTTP request pipeline =====

    // Middleware de logging de requests
    app.UseSerilogRequestLogging();

    // Configurar Swagger (disponible en tots els entorns)
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "VeteriLach Read API v1");
        options.RoutePrefix = "swagger"; // Accés a /swagger
        options.DocumentTitle = "VeteriLach API - Documentació";
    });

    // CORS
    app.UseCors("AllowAll");

    // Middleware d'API Key Authentication (ABANS de UseAuthorization)
    app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("VeteriLach.ReadApi iniciat correctament");
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "L'aplicació ha fallat durant l'inici");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
