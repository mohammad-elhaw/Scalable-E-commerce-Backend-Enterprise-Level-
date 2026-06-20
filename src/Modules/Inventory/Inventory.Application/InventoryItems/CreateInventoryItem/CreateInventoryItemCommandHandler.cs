using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using Inventory.Domain.Warehouses;
using SharedKernel;

namespace Inventory.Application.InventoryItems.CreateInventoryItem;

internal class CreateInventoryItemCommandHandler(
    IInventoryItemRepository repository)
    : ICommandHandler<CreateInventoryItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        var variantId = new ProductVariantId(command.ProductVariantId);
        var warehouseId = new WarehouseId(command.WarehouseId);

        var existing = await repository
            .GetByVariantAndWarehouseAsync(variantId, warehouseId, cancellationToken);

        if (existing is not null)
            return Result<Guid>.Failure(InventoryErrors.DuplicateSkuInWarehouse);

        var inventoryResult = InventoryItem.Create(variantId, warehouseId);

        if (inventoryResult.IsFailure)
            return Result<Guid>.Failure(inventoryResult.Error);

        repository.Add(inventoryResult.Value!);

        await repository.SaveChanges();

        return Result<Guid>.Success(inventoryResult.Value!.Id.Value);
    }
}
