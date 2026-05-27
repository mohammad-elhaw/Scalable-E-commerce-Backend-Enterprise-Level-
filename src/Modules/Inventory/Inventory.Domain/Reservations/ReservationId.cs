namespace Inventory.Domain.Reservations;

public sealed record ReservationId(Guid Value)
{
    public static ReservationId New() => new(Guid.NewGuid());
}