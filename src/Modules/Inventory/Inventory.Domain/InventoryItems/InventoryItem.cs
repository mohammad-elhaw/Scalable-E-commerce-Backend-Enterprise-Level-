using Inventory.Domain.Errors;
using Inventory.Domain.Warehouses;
using SharedKernel;

namespace Inventory.Domain.InventoryItems;

public class InventoryItem : AuditableAggregateRoot<InventoryItemId>
{
    public ProductVariantId ProductVariantId { get; private set; }
    public WarehouseId WarehouseId { get; private set; }
    public StockQuantity QuantityOnHand { get; private set; }
    public StockQuantity ReservedQuantity { get; private set; }

    public bool IsActive { get; private set; }
    public int AvailableQuantity => QuantityOnHand.Value - ReservedQuantity.Value;

    private readonly List<InventoryTransaction> _transactions = [];
    public IReadOnlyList<InventoryTransaction> Transactions => _transactions.AsReadOnly();

    private InventoryItem() { }

    public static Result<InventoryItem> Create(
        ProductVariantId productVariantId,
        WarehouseId warehouseId)
    {
        var zero = StockQuantity.Create(0).Value!;

        if(Guid.Empty == productVariantId.Value)
            return Result<InventoryItem>.Failure(InventoryErrors.InvalidProductVariantId);

        if(Guid.Empty == warehouseId.Value)
            return Result<InventoryItem>.Failure(InventoryErrors.InvalidWarehouseId);

        var inventoryItem = new InventoryItem
        {
            Id = InventoryItemId.New(),
            ProductVariantId = productVariantId,
            WarehouseId = warehouseId,
            QuantityOnHand = zero,
            ReservedQuantity = zero,
            IsActive = true
        };

        return Result<InventoryItem>.Success(inventoryItem);
    }

    public Result AddStock(StockQuantity quantity, string? note)
    {
        QuantityOnHand = QuantityOnHand.Increase(quantity.Value).Value!;
        
        AddTransaction(InventoryTransactionType.StockAdded, quantity.Value, note);
        // raise domain event for stock added
        return Result.Success();
    }

    public Result RemoveStock(StockQuantity quantity, string? note)
    {
        QuantityOnHand = QuantityOnHand.Decrease(quantity.Value).Value!;

        AddTransaction(InventoryTransactionType.StockRemoved, quantity, note);
        // raise domain event for stock removed
        return Result.Success();
    }

    public Result ReserveStock(StockQuantity quantity, string? note)
    {
        if (AvailableQuantity < quantity.Value)
            return Result.Failure(InventoryErrors.ReservationExceedsAvailableStock);

        ReservedQuantity = ReservedQuantity.Increase(quantity.Value).Value!;

        AddTransaction(InventoryTransactionType.Reserved, quantity.Value, note);
        // raise domain event for stock reserved
        return Result.Success();
    }

    // after payment should be called when reservation is confirmed,
    // it will decrease both reserved quantity and quantity on hand
    public Result CommitReservation(int quantity, string? note)
    {
        if (quantity < 0)
            return Result<StockQuantity>.Failure(InventoryErrors.InvalidQuantity);

        if (ReservedQuantity.Value < quantity)
            return Result.Failure(InventoryErrors.InvalidQuantity);

        ReservedQuantity = ReservedQuantity.Decrease(quantity).Value!;
        QuantityOnHand = QuantityOnHand.Decrease(quantity).Value!;

        AddTransaction(InventoryTransactionType.Committed, quantity, note);
        
        return Result.Success();
    }

    public Result ReleaseReservation(int quantity, string? note)
    {
        if (quantity < 0)
            return Result<StockQuantity>.Failure(InventoryErrors.InvalidQuantity);

        if (ReservedQuantity.Value < quantity)
            return Result.Failure(InventoryErrors.InvalidQuantity);

        ReservedQuantity = ReservedQuantity.Decrease(quantity).Value!;
        AddTransaction(InventoryTransactionType.Released, quantity, note);
        // raise domain event for stock released
        return Result.Success();
    }

    // should used by admin
    public Result AdjustStock(
        int newQuantity,
        string? note = null)
    {
        if(newQuantity < 0)
            return Result.Failure(InventoryErrors.InvalidAdjustment);
        
        if(newQuantity < ReservedQuantity.Value)
            return Result.Failure(InventoryErrors.AdjustmentLessThanReserved);

        var difference = newQuantity - QuantityOnHand.Value;

        QuantityOnHand = StockQuantity.Create(newQuantity).Value!;
        AddTransaction(InventoryTransactionType.Adjusted, difference, note);
        // raise domain event for stock adjusted
        return Result.Success();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void DeActivate() 
    { 
        IsActive = false; 
    }

    private void AddTransaction(
        InventoryTransactionType type,
        int quantity,
        string? note)
    {
        _transactions.Add(
            new InventoryTransaction(type, quantity, note));
    }

}