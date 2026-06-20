using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Application.InventoryItems.ReleaseReservation;

internal class ReleaseReservationCommandHandler(
    IInventoryItemRepository repository)
    : ICommandHandler<ReleaseReservationCommand>
{
    public async Task<Result> Handle(ReleaseReservationCommand command, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetByIdAsync(
            new InventoryItemId(command.InventoryItemId), 
            cancellationToken);

        if (inventory is null)
            return Result.Failure(InventoryErrors.InventoryItemNotFound);

        var result = inventory.ReleaseReservation(command.Quantity, command.Note);
        if (result.IsFailure)
            return Result.Failure(result.Error);

        await repository.SaveChanges();

        return Result.Success();
    }
}
