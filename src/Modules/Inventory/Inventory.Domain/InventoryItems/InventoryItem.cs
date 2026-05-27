using Inventory.Domain.Errors;
using Inventory.Domain.Warehouses;
using SharedKernel;

namespace Inventory.Domain.InventoryItems;

public class InventoryItem : AuditableAggregateRoot<InventoryItemId>
{
    private readonly List<InventoryTransaction> _transactions = [];

    public ProductVariantId ProductVariantId { get; private set; }
    public WarehouseId WarehouseId { get; private set; }
    public StockQuantity QuantityOnHand { get; private set; }
    public StockQuantity ReservedQuantity { get; private set; }

    public bool IsActive { get; private set; }

    public int AvailableQuantity => QuantityOnHand.Value - ReservedQuantity.Value;

    public IReadOnlyList<InventoryTransaction> Transactions => _transactions.AsReadOnly();

    private InventoryItem() { }

    public static Result<InventoryItem> Create(
        ProductVariantId productVariantId,
        WarehouseId warehouseId)
    {
        var zero = StockQuantity.Create(0).Value!;

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

        return Result.Success();
    }

    public Result RemoveStock(StockQuantity quantity, string? note)
    {
        QuantityOnHand = QuantityOnHand.Decrease(quantity.Value).Value!;

        AddTransaction(InventoryTransactionType.StockRemoved, quantity, note);

        return Result.Success();
    }

    public Result Reserve(StockQuantity quantity, string? note)
    {
        if (AvailableQuantity < quantity.Value)
            return Result.Failure(InventoryErrors.ReservationExceedsAvailableStock);

        ReservedQuantity = ReservedQuantity.Increase(quantity.Value).Value!;

        AddTransaction(InventoryTransactionType.Reserved, quantity.Value, note);
        return Result.Success();
    }

    public Result ReleaseReservation(StockQuantity quantity, string? note)
    {
        if (ReservedQuantity.Value < quantity.Value)
            return Result.Failure(InventoryErrors.InvalidQuantity);

        ReservedQuantity = ReservedQuantity.Decrease(quantity.Value).Value!;
        AddTransaction(InventoryTransactionType.Released, quantity.Value, note);

        return Result.Success();
    }

    public Result AdjustStock(
        int newQuantity,
        string? note = null)
    {
        if(newQuantity < 0)
            return Result.Failure(InventoryErrors.InvalidAdjustment);
        
        if(newQuantity < ReservedQuantity.Value)
            return Result.Failure(InventoryErrors.AdjustmentLessThanReserved);

        var difference = newQuantity - ReservedQuantity.Value;

        QuantityOnHand = StockQuantity.Create(newQuantity).Value!;
        AddTransaction(InventoryTransactionType.Adjusted, difference, note);

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