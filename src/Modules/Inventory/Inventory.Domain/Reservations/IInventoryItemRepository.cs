using Inventory.Domain.InventoryItems;
using Inventory.Domain.Warehouses;
using SharedKernel;

namespace Inventory.Domain.Reservations;

public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(InventoryItemId inventoryItemId, 
        CancellationToken cancellationToken);

    Task<InventoryItem?> GetByVariantAndWarehouseAsync(
        ProductVariantId variantId, 
        WarehouseId warehouseId,
        CancellationToken cancellationToken);

    Task AddAsync(
        InventoryItem inventoryItem,
        CancellationToken cancellationToken);

    void Remove(InventoryItem inventoryItem);
}