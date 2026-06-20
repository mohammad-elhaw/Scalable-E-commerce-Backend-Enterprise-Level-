using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.ActivateInventoryItem;

public sealed record ActivateInventoryItemCommand(
    Guid InventoryItemId) : ICommand;