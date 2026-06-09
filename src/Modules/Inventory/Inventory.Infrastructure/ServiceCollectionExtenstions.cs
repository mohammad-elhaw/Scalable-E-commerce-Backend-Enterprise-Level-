using Infrastructure.EventBus;
using Inventory.Application.Reservations.Services;
using Inventory.Infrastructure.BackgroundJobs;
using Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure;

public static class ServiceCollectionExtenstions
{
    public static IServiceCollection AddInventoryInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(
            options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });
        
        // Application Services
        services.AddScoped<IReservationExpirationService, ReservationExpirationService>();

        // Repositories

        // Background Jobs
        //services.AddHostedService<ReservationExpirationWorker>();

        // Cap 
        services.AddCap<InventoryDbContext>(configuration);

        return services;
    }
}
