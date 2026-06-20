using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.DeactivateInventoryItem;

public sealed record DeactivateInventoryItemCommand(
    Guid InventoryItemId): ICommand ;