using SharedKernel;

namespace Inventory.Domain.InventoryItems;

public sealed class InventoryTransaction : Entity<Guid>
{
    public InventoryTransactionType Type { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public string? Note { get; private set; }

    private InventoryTransaction() { }

    internal InventoryTransaction(
        InventoryTransactionType type,
        int quantity,
        string? note = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        Quantity = quantity;
        CreatedAtUtc = DateTime.UtcNow;
        Note = note;
    }
}