namespace Inventory.API.InventoryItems.CreateInventoryItems;

public sealed record Request(
    Guid ProductVariantId,
    Guid WarehouseId);