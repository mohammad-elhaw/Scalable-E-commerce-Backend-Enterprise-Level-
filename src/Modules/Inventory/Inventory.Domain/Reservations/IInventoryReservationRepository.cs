namespace Inventory.Domain.Reservations;

public interface IInventoryReservationRepository
{
    Task<InventoryReservation?> GetByIdAsync(
        ReservationId reservationId,
        CancellationToken cancellationToken);

    Task<List<InventoryReservation>> GetExpiredAsync(
        CancellationToken cancellationToken);

    void Add(InventoryReservation reservation);
    void Remove(InventoryReservation reservation);
}