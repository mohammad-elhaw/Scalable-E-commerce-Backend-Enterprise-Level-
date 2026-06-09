namespace Inventory.Application;

public sealed class InventoryAvailabilityReadModel
{
    public Guid ProductVariantId { get; init; }
    public int TotalOnHand { get; init; }
    public int TotalReserved { get; init; }
    public int TotalAvailable { get; init; }
}