using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.ReserveInventory;

public sealed record ReserveInventoryCommand(
    Guid InventoryItemId,
    Guid OrderId,
    int Quantity,
    DateTime ExpiresAtUtc): ICommand<Guid>;