namespace Inventory.Domain.Warehouses;

public sealed record WarehouseId(Guid Value)
{
    public static WarehouseId New()
        => new(Guid.NewGuid());
}