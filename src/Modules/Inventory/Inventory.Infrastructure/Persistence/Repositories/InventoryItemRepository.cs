using Inventory.Domain.InventoryItems;
using Inventory.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class InventoryItemRepository(InventoryDbContext context)
    : IInventoryItemRepository
{
    public void Add(InventoryItem inventoryItem)
        => context.InventoryItems.Add(inventoryItem);

    public async Task<InventoryItem?> GetByIdAsync(InventoryItemId inventoryItemId, 
        CancellationToken cancellationToken)
        => await context.InventoryItems.FindAsync([inventoryItemId], cancellationToken);

    public async Task<InventoryItem?> GetByVariantAndWarehouseAsync(
        ProductVariantId variantId, 
        WarehouseId warehouseId,
        CancellationToken cancellationToken)
        => await context.InventoryItems
            .FirstOrDefaultAsync(i => i.ProductVariantId == variantId
                && i.WarehouseId == warehouseId, cancellationToken);

    public void Remove(InventoryItem inventoryItem)
        => context.InventoryItems.Remove(inventoryItem);

    public async Task<int> SaveChanges()
        => await context.SaveChangesAsync();
}