namespace Inventory.Application;

public enum InventorySortBy : byte
{
    WarehouseName = 1,
    AvailableQuantity = 2,
    QuantityOnHand = 3,
    ReservedQuantity = 4,
    CreatedAt = 5
}