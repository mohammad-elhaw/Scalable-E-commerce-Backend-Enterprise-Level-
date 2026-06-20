using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.ReleaseReservation;

public sealed record ReleaseReservationCommand(
    Guid InventoryItemId,
    int Quantity,
    string? Note): ICommand;