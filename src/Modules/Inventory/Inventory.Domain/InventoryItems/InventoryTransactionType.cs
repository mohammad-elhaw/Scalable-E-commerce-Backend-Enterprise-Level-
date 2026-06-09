namespace Inventory.Domain.InventoryItems;

public enum InventoryTransactionType : byte
{
    StockAdded = 1,
    StockRemoved = 2,
    Reserved = 3,
    Released = 4,
    Adjusted = 5,
    Committed = 6
}