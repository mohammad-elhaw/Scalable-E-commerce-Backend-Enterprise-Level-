using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public class ProductVariant : Entity<int>
{
    public string Sku { get; private set; } = string.Empty;
    public Money Price { get; private set; } = default!;
    public Weight Weight { get; private set; } = default!;
    public Dimensions Dimensions { get; private set; } = default!;

    private readonly List<VariantAttribute> _attributes = [];
    public IReadOnlyCollection<VariantAttribute> Attributes => _attributes.AsReadOnly();
    
    private ProductVariant() { }

    public static Result<ProductVariant> Create(string sku, Money price, Weight weight, Dimensions dimensions)
    {
        if (string.IsNullOrWhiteSpace(sku))
            return Result<ProductVariant>.Failure(ProductVariantError.EmptySku);

        var priceResult = price.IsLessThanOrEqualTo(Money.Zero(price.Currency));

        if (priceResult.Value)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidPrice);

        var weightUnitResult = weight.IsLessThanOrEqual(Weight.Zero(weight.Unit));
        if (weightUnitResult.IsFailure)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidWeight);

        if (weightUnitResult.Value)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidWeight);
        
        if (dimensions.IsZero)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidDimensions);

        var variant = new ProductVariant
        {
            Sku = sku,
            Price = price,
            Weight = weight,
            Dimensions = dimensions
        };
        return Result<ProductVariant>.Success(variant);
    }

    public Result AddAttribute(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(ProductVariantError.EmptyAttributeName);
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure(ProductVariantError.EmptyAttributeValue);

        var attributeResult = VariantAttribute.Create(name, value);
        if(attributeResult.IsFailure)
            return Result.Failure(attributeResult.Error);

        var exists = _attributes.Any(x => x.Name == attributeResult.Value!.Name);
        if (exists)
            return Result.Failure(ProductVariantError.EmptyAttributeName);

        _attributes.Add(attributeResult.Value!);

        return Result.Success();
    }

    public Result RemoveAttribute(string name)
    {
        var attribute = _attributes
            .FirstOrDefault(x => x.Name == name.Trim().ToLowerInvariant());
        
        if (attribute is null)
            return Result.Failure(ProductVariantError.AttributeNotFound);

        _attributes.Remove(attribute);
        return Result.Success();
    }

    public Result UpdatePrice(Money newPrice)
    {
        var priceResult = newPrice.IsLessThanOrEqualTo(Money.Zero(newPrice.Currency));
        if (priceResult.Value)
            return Result.Failure(ProductVariantError.InvalidPrice);
        Price = newPrice;
        return Result.Success();
    }
}
