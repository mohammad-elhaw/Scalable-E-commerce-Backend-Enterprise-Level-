using Inventory.Domain.Reservations;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class InventoryReservationRepository(
    InventoryDbContext context)
    : IInventoryReservationRepository
{
    public void Add(InventoryReservation reservation)
        => context.InventoryReservations.Add(reservation);

    public async Task<List<InventoryReservation>> GetExpiredAsync(
        DateTime utcNow,
        CancellationToken cancellationToken)
        => await context.InventoryReservations
            .Where(x =>
                x.Status == ReservationStatus.Active &&
                x.ExpiresAtUtc <= utcNow && !x.IsExpired(utcNow))
            .ToListAsync(cancellationToken);

    public async Task<InventoryReservation?> GetByIdAsync(ReservationId reservationId, CancellationToken cancellationToken)
        => await context.InventoryReservations.SingleOrDefaultAsync(
            x => x.Id == reservationId, cancellationToken);

    public void Remove(InventoryReservation reservation)
        => context.InventoryReservations.Remove(reservation);
}
