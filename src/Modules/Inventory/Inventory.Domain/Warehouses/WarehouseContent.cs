using Inventory.Domain.Errors;
using SharedKernel;

namespace Inventory.Domain.Warehouses;

public sealed class WarehouseContent
    : ValueObject
{
    internal string Name { get; }
    internal string Code { get; }

    private WarehouseContent(string name, string code)
    {
        Name = name;
        Code = code;
    }
    public static Result<WarehouseContent> Create(
        string name,
        string code)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<WarehouseContent>.Failure(InventoryErrors.InvalidWarehouse);

        name = name.Trim();
        if (name.Length > 100)
            return Result<WarehouseContent>.Failure(InventoryErrors.InvalidWarehouse);

        if (string.IsNullOrWhiteSpace(code))
            return Result<WarehouseContent>.Failure(InventoryErrors.InvalidWarehouse);

        code = code.Trim().ToUpperInvariant();

        return Result<WarehouseContent>.Success(new WarehouseContent(name, code));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Code;
    }
}
