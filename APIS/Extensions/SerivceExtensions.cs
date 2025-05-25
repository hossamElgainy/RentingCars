
using System.Data;
using Core.Dtos.JWT;
using Core.Interfaces.IServices.SystemIServices;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specifications;
using Hangfire;
using Infrastructure.Data;
using Infrastructure.Data.Seeding;
using Infrastructure.Data.Specification;
using Infrastructure.Data.UnitOfWork;
using Infrastructure.Services.SystemServices;
using Infrastructure.Services.UsersServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Solution1.Core.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RentingCars.APIS.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddFrameworkServices(services);
            ConfigureLogging(services, configuration);
            RegisterApplicationServices(services);
            ConfigureDatabase(services, configuration);
            ConfigureEmailSettings(services, configuration);
            ConfigureJwtSettings(services, configuration);
            ConfigureCors(services);
            ConfigureSwagger(services);
            ConfigureHangFire(services,configuration);

            return services;
        }

        private static void AddFrameworkServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSignalR();
        }

        private static void ConfigureLogging(IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .Enrich.With<SolvedLogEnricher>()
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("Default"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "LogModel",
                        AutoCreateSqlTable = false,
                        SchemaName = "dbo"
                    },
                    columnOptions: GetColumnOptions())
                .CreateLogger();

            Log.Logger = logger;
            services.AddSingleton<Serilog.ILogger>(logger);
            services.AddScoped<ILoggerService, LoggerService>();
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            // Core Services
            services.AddScoped<ClaimService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // File Service with special configuration
            services.AddScoped<IFileService>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<FileService>>();
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                return new FileService(logger, webRootPath);
            });




            // Infrastructure Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<BookingCompletionService>();

            // Utilities
            services.AddSingleton<SolvedLogEnricher>();
            services.AddScoped<DataSeeder>();            
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
        }

        private static void ConfigureEmailSettings(IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailSettings>();
            services.AddSingleton(emailConfig);
        }

        private static void ConfigureJwtSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JwtSettings"));
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                ConfigureSwaggerDocument(swagger);
                ConfigureSecurityDefinitions(swagger);
                ConfigureSecurityRequirements(swagger);
            });
        } 
        private static void ConfigureHangFire(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
                config.UseSqlServerStorage(configuration.GetConnectionString("Default")));

            services.AddHangfireServer();
        }


        private static void ConfigureSwaggerDocument(SwaggerGenOptions swagger)
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "HR APIS",
                Description = "ASP.NET Core 8 Web API"
            });
        }

        private static void ConfigureSecurityDefinitions(SwaggerGenOptions swagger)
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            swagger.AddSecurityDefinition("Accept-Language", new OpenApiSecurityScheme
            {
                Name = "Accept-Language",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "Enter 'ar' for Arabic or 'en' for English"
            });
        }

        private static void ConfigureSecurityRequirements(SwaggerGenOptions swagger)
        {
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Accept-Language"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }



        private static ColumnOptions GetColumnOptions()
        {
            var columnOptions = new ColumnOptions();

            // Remove all default columns
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Remove(StandardColumn.LogEvent);

            // Configure required columns
            columnOptions.Id.ColumnName = "Id";
            columnOptions.Id.DataType = SqlDbType.Int;

            columnOptions.Message.ColumnName = "Message";
            columnOptions.Message.DataLength = -1; // MAX length

            columnOptions.Level.ColumnName = "Level";
            columnOptions.Level.DataLength = 50;

            columnOptions.TimeStamp.ColumnName = "TimeStamp";

            columnOptions.Exception.ColumnName = "Exception";
            columnOptions.Exception.DataLength = -1;

            columnOptions.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn("Solved", SqlDbType.Bit)
            };

            return columnOptions;
        }
      
    }
}