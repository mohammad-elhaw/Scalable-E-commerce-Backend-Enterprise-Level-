using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Application.InventoryItems.DeactivateInventoryItem;

internal class DeactivateInventoryItemCommandHandler(
    IInventoryItemRepository repository)
    : ICommandHandler<DeactivateInventoryItemCommand>
{
    public async Task<Result> Handle(DeactivateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        var inventoryItem = await repository.GetByIdAsync(
            new InventoryItemId(request.InventoryItemId),
            cancellationToken);

        if (inventoryItem is null)
            return Result.Failure(InventoryErrors.InventoryItemNotFound);

        inventoryItem.DeActivate();

        await repository.SaveChanges();
        return Result.Success();
    }
}
