namespace Inventory.Domain.Reservations;

public enum ReservationStatus : byte
{
    Active = 1,
    Confirmed = 2,
    Cancelled = 3,
    Expired = 4
}