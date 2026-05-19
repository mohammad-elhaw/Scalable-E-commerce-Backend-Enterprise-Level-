using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class Weight : ValueObject
{
    public decimal Value { get; }
    public WeightUnit Unit { get; }

    public static Weight Zero(WeightUnit weightUnit) => new(0, weightUnit);

    private Weight(decimal value, WeightUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Result<Weight> Create(decimal value, WeightUnit unit)
    {
        if (value < 0)
            return Result<Weight>.Failure(ProductVariantError.InvalidWeight);

        if (!Enum.IsDefined(unit))
            return Result<Weight>.Failure(ProductVariantError.InvalidWeightUnit);

        return Result<Weight>.Success(new Weight(value, unit));
    }

    public Result<bool> IsLessThanOrEqual(Weight other)
    {
        if (Unit != other.Unit)
            return Result<bool>.Failure(ProductVariantError.InvalidWeightUnit);

        return Result<bool>.Success(Value <= other.Value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Unit;
    }
}
