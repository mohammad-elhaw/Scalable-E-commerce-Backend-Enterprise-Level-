using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using Inventory.Domain.Reservations;
using SharedKernel;

namespace Inventory.Application.InventoryItems.ReserveInventory;

internal class ReserveInventoryCommandHandler(
    IInventoryItemRepository inventoryRepository,
    IInventoryReservationRepository reservationRepository): ICommandHandler<ReserveInventoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ReserveInventoryCommand command, CancellationToken cancellationToken)
    {
        var inventory = await inventoryRepository.GetByIdAsync(
            new InventoryItemId(command.InventoryItemId), 
            cancellationToken);

        if (inventory is null)
            return Result<Guid>.Failure(InventoryErrors.InventoryItemNotFound);

        var quantityResult = StockQuantity.Create(command.Quantity);
        if (quantityResult.IsFailure)
            return Result<Guid>.Failure(quantityResult.Error);

        var reserveResult =
            inventory.ReserveStock(quantityResult.Value!,
            $"Order {command.OrderId}");

        if (reserveResult.IsFailure)
            return Result<Guid>.Failure(reserveResult.Error);

        var reservationResult = InventoryReservation.Create(
            inventory.Id,
            command.OrderId,
            quantityResult.Value!,
            command.ExpiresAtUtc);

        if (reservationResult.IsFailure)
            return Result<Guid>.Failure(reservationResult.Error);

        reservationRepository.Add(reservationResult.Value!);

        // we would typically use a Unit of Work to ensure atomicity of the operations

        return Result<Guid>.Success(reservationResult.Value!.Id.Value);
    }
}
