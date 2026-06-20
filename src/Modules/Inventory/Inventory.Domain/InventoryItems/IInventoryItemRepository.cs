using Inventory.Domain.Warehouses;
using SharedKernel;

namespace Inventory.Domain.InventoryItems;

public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(InventoryItemId inventoryItemId, 
        CancellationToken cancellationToken);

    Task<InventoryItem?> GetByVariantAndWarehouseAsync(
        ProductVariantId variantId, 
        WarehouseId warehouseId,
        CancellationToken cancellationToken);

    void Add(InventoryItem inventoryItem);
    void Remove(InventoryItem inventoryItem);
    Task<int> SaveChanges();
}