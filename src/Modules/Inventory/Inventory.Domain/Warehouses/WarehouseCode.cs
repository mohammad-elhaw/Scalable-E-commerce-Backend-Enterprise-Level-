using Inventory.Domain.Errors;
using SharedKernel;

namespace Inventory.Domain.Warehouses;

public sealed class WarehouseCode : ValueObject
{
    public string Value { get; }

    private WarehouseCode(string value)
    {
        Value = value;
    }

    public static Result<WarehouseCode> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<WarehouseCode>.Failure(InventoryErrors.InvalidWarehouse);

        value = value.Trim().ToUpperInvariant();

        return Result<WarehouseCode>.Success(new WarehouseCode(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(WarehouseCode code) => code.Value;
}