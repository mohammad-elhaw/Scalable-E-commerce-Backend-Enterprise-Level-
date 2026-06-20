using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.AdjustStock;

public sealed record AdjustStockCommand(
    Guid InventoryItemId,
    int NewQuantity,
    string? Note): ICommand;