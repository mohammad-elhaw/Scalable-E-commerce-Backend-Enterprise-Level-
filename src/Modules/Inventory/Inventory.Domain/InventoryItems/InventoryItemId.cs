namespace Inventory.Domain.InventoryItems;

public sealed record InventoryItemId(Guid Value)
{
    public static InventoryItemId New() => new(Guid.NewGuid());
}
