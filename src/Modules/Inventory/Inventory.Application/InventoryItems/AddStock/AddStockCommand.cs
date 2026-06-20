using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.AddStock;

public sealed record AddStockCommand(
    Guid InventoryItemId,
    int Quantity,
    string? Note) : ICommand;