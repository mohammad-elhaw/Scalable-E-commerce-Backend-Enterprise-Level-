namespace Inventory.Application.Reservations.Services;

public interface IReservationExpirationService
{
    Task ProcessExpiredReservationsAsync(CancellationToken cancellationToken);
}
