using Infrastructure.EventBus;
using Inventory.Application.Reservations.Services;
using Inventory.Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure;

public static class ServiceCollectionExtenstions
{
    public static IServiceCollection AddInventoryInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Application Services
        services.AddScoped<IReservationExpirationService, ReservationExpirationService>();

        // Repositories

        // Background Jobs
        services.AddHostedService<ReservationExpirationWorker>();

        // Cap 
        services.AddCap<InventoryDbContext>(configuration);

        return services;
    }
}
