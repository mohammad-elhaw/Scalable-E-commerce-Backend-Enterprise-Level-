using Inventory.Domain.Errors;
using SharedKernel;

namespace Inventory.Domain.InventoryItems;

public sealed class StockQuantity : ValueObject
{
    public int Value { get; }

    private StockQuantity(int value)
    {
        Value = value;
    }

    public static Result<StockQuantity> Create(int value)
    {
        if (value < 0)
            return Result<StockQuantity>.Failure(InventoryErrors.InvalidQuantity);
        
        return Result<StockQuantity>.Success(new StockQuantity(value));
    }

    public Result<StockQuantity> Increase(int amount)
    {
        if(amount < 0)
            return Result<StockQuantity>.Failure(InventoryErrors.InvalidQuantity);

        return Result<StockQuantity>.Success(new StockQuantity(Value + amount));
    }

    public Result<StockQuantity> Decrease(int amount)
    {
        if (amount > Value)
            return Result<StockQuantity>.Failure(InventoryErrors.InsufficientStock);

        return Result<StockQuantity>.Success(new StockQuantity(Value - amount));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(StockQuantity quantity) => quantity.Value;
}
