namespace Inventory.Application;

public sealed record InventoryItemReadModel
{
    public Guid Id { get; init; }
    public Guid ProductVariantId { get; init; }
    public Guid WarehouseId { get; init; }
    public string WarehouseCode { get; init; } = string.Empty;
    public string WarehouseName { get; init; } = string.Empty;
    public int QuantityOnHand { get; init; }
    public int ReservedQuantity { get; init; }
    public int AvailableQuantity { get; init; }
    public bool IsActive { get; init; }
}