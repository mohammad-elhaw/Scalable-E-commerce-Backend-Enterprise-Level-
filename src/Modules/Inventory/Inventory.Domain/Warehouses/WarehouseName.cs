using Inventory.Domain.Errors;
using SharedKernel;

namespace Inventory.Domain.Warehouses;

public sealed class WarehouseName : ValueObject
{
    public string Value { get; }

    private WarehouseName(string value)
    {
        Value = value;
    }

    public static Result<WarehouseName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<WarehouseName>.Failure(InventoryErrors.InvalidWarehouse);

        value = value.Trim();
        if(value.Length > 100)
            return Result<WarehouseName>.Failure(InventoryErrors.InvalidWarehouse);

        return Result<WarehouseName>.Success(new WarehouseName(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(WarehouseName name) => name.Value;
}
