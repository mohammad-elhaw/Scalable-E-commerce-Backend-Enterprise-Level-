using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class Dimensions : ValueObject
{
    public decimal Width { get; }
    public decimal Height { get; }
    public decimal Length { get; }
    public DimensionUnit Unit { get; }

    private Dimensions(
        decimal width,
        decimal height,
        decimal length,
        DimensionUnit unit)
    {
        Width = width;
        Height = height;
        Length = length;
        Unit = unit;
    }

    public static Result<Dimensions> Create(
        decimal width,
        decimal height,
        decimal length,
        DimensionUnit unit)
    {
        if (width <= 0 || height <= 0 || length <= 0)
            return Result<Dimensions>.Failure(ProductVariantError.InvalidDimensions);

        return Result<Dimensions>.Success(
            new Dimensions(
                decimal.Round(width, 2),
                decimal.Round(height, 2),
                decimal.Round(length, 2),
                unit));
    }

    public decimal Volume()
        => Width * Height * Length;

    public bool IsZero =>
        Width == 0 &&
        Height == 0 &&
        Length == 0;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Width;
        yield return Height;
        yield return Length;
        yield return Unit;
    }
}
