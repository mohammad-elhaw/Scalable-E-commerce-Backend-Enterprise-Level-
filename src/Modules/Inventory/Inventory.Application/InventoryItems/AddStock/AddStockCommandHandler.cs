using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Application.InventoryItems.AddStock;

internal class AddStockCommandHandler(
    IInventoryItemRepository repository)
    : ICommandHandler<AddStockCommand>
{
    public async Task<Result> Handle(AddStockCommand command, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(
            new InventoryItemId(command.InventoryItemId), 
            cancellationToken);

        if (inventory is null)
            return Result.Failure(InventoryErrors.InventoryItemNotFound);

        var quantityResult = StockQuantity.Create(command.Quantity);
        if(quantityResult.IsFailure)
            return Result.Failure(quantityResult.Error);

        var result = inventory.AddStock(quantityResult.Value!, command.Note);

        if(result.IsFailure)
            return Result.Failure(result.Error);

        await repository.SaveChanges();

        return Result.Success();
    }
}
