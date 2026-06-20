using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Application.InventoryItems.ActivateInventoryItem;

internal class ActivateInventoryItemCommandHandler(
    IInventoryItemRepository repository)
    : ICommandHandler<ActivateInventoryItemCommand>
{
    public async Task<Result> Handle(ActivateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(
            new InventoryItemId(command.InventoryItemId),
            cancellationToken);
    
        if(inventory is null)
            return Result.Failure(InventoryErrors.InventoryItemNotFound);

        inventory.Activate();
        await repository.SaveChanges();
        return Result.Success();
    }
}