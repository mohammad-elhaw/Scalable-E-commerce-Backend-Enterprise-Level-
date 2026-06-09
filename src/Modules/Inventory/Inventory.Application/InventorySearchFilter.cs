namespace Inventory.Application;

public sealed class InventorySearchFilter
{
    public Guid? ProductVariantId { get; init; }
    public Guid? WarehouseId { get; init; }
    public bool? IsActive { get; init; }
    public bool? TrackInventory { get; init; }
    public int? MinimumAvailableQuantity { get; init; }
    public int? MaximumAvailableQuantity { get; init; }
    public string? WarehouseCode { get; init; }
    public string? WarehouseName { get; init; }

    public InventorySortBy SortBy { get; init; }
        = InventorySortBy.WarehouseName;

    public SortDirection SortDirection { get; init; }
        = SortDirection.Asc;

    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public int Offset => (Page - 1) * PageSize;
}