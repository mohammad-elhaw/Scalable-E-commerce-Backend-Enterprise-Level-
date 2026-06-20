using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.CommitReservation;

public sealed record CommitReservationCommand(
    Guid ReservationId): ICommand;