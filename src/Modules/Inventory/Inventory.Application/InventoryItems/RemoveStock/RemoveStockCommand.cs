using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.RemoveStock;

public sealed record RemoveStockCommand(
    Guid InventoryItemId,
    int Quantity,
    string? Note): ICommand;