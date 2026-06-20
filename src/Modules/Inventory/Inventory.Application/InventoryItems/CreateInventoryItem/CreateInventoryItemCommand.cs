using Application.Abstractions.Messaging;

namespace Inventory.Application.InventoryItems.CreateInventoryItem;

public record CreateInventoryItemCommand(
    Guid ProductVariantId,
    Guid WarehouseId)
    : ICommand<Guid>;