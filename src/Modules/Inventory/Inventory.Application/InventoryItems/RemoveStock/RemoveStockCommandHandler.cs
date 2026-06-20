using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Application.InventoryItems.RemoveStock;

internal class RemoveStockCommandHandler(
    IInventoryItemRepository repository): ICommandHandler<RemoveStockCommand>
{
    public async Task<Result> Handle(RemoveStockCommand command, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(
            new InventoryItemId(command.InventoryItemId), 
            cancellationToken);

        if (inventory is null)
            return Result.Failure(InventoryErrors.InventoryItemNotFound);

        var quantityResult = StockQuantity.Create(command.Quantity);

        if(quantityResult.IsFailure)
            return Result.Failure(quantityResult.Error);

        inventory.RemoveStock(quantityResult.Value!, command.Note);

        await repository.SaveChanges();

        return Result.Success();
    }
}