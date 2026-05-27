using Inventory.Application.Reservations.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inventory.Infrastructure.BackgroundJobs;

public class ReservationExpirationWorker(
    IServiceScopeFactory scopeFactory)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();

            var service = scope.ServiceProvider
                .GetRequiredService<IReservationExpirationService>();

            await service.ProcessExpiredReservationsAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}
