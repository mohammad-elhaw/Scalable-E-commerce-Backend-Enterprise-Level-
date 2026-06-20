namespace Inventory.Domain.Reservations;

public enum ReservationStatus : byte
{
    Active = 1,
    Committed = 2,
    Released = 3,
    Expired = 4
}