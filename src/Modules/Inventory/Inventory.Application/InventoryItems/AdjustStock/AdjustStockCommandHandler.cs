using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Application.InventoryItems.AdjustStock;

internal class AdjustStockCommandHandler(
    IInventoryItemRepository repository): ICommandHandler<AdjustStockCommand>
{
    public async Task<Result> Handle(AdjustStockCommand command, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(
            new InventoryItemId(command.InventoryItemId), cancellationToken);

        if (inventory == null)
            return Result.Failure(InventoryErrors.InventoryItemNotFound);

        var result = inventory.AdjustStock(command.NewQuantity, command.Note);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        await repository.SaveChanges();

        return Result.Success();
    }
}