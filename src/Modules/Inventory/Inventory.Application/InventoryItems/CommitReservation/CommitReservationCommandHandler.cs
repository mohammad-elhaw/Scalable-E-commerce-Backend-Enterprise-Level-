using Application.Abstractions.Messaging;
using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using Inventory.Domain.Reservations;
using SharedKernel;

namespace Inventory.Application.InventoryItems.CommitReservation;

internal class CommitReservationCommandHandler(
    IInventoryReservationRepository reservationRepository,
    IInventoryItemRepository inventoryRepository): ICommandHandler<CommitReservationCommand>
{
    public async Task<Result> Handle(CommitReservationCommand command, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository
            .GetByIdAsync(new ReservationId(command.ReservationId),
            cancellationToken);

        if (reservation is null)
            return Result.Failure(ReservationErrors.ReservationNotFound);

        var inventory = await inventoryRepository
            .GetByIdAsync(reservation.InventoryItemId,
            cancellationToken);

        if (inventory is null)
            return Result.Failure(InventoryErrors.InventoryItemNotFound);

        var commitReservationResult =
            inventory.CommitReservation(
                reservation.ReservationQuantity,
            $"Order {reservation.OrderId}");

        if(commitReservationResult.IsFailure)
            return Result.Failure(commitReservationResult.Error);

        reservation.Commit();

        // we would use unit of work pattern here to commit both changes in a single transaction

        return Result.Success();
    }
}
