using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Domain.Reservations;

public sealed record ReservationExpiredDomainEvent(
    ReservationId ReservationId,
    InventoryItemId InventoryItemId,
    int Quantity) : IDomainEvent
{
    public DateTime OccuredOn => DateTime.UtcNow;
}