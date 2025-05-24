using System;
using Core.Interfaces.IServices;
using Hangfire;
using Infrastructure.Data;
using Infrastructure.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RentingCars.APIS.Extensions;
using Solution1.Core.Settings;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor(); 

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddJwtAuthentication(builder.Configuration);
        var app = builder.Build();
        app.MapHangfireDashboard();
        #region Migrate database

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var dbContext = services.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, ex.Message);
        }

        #endregion
        app.UseCustomMiddleware();
        using (var seedScope = app.Services.CreateScope())
        {
            var seeder = seedScope.ServiceProvider.GetRequiredService<DataSeeder>();
            seeder.SeedAsync().GetAwaiter().GetResult();
        }

        app.Run();
    }

}
