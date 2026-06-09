using Application.Abstractions.Pagination;

namespace Inventory.Application;

public interface IInventoryQueries
{
    Task<InventoryItemReadModel?> GetByIdAsync(
        Guid inventoryItemId,
        CancellationToken cancellationToken);

    Task<InventoryItemReadModel?> GetByVariantAsync(
        Guid productVariantId,
        Guid warehouseId,
        CancellationToken cancellationToken);

    Task<PagedResult<InventoryItemReadModel>>
        SearchAsync(
            InventorySearchFilter filter,
            CancellationToken cancellationToken);

    Task<InventoryAvailabilityReadModel?>
        GetAvailabilityAsync(
            Guid productVariantId,
            CancellationToken cancellationToken);
}
