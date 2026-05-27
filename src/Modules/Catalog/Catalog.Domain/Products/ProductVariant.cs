using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public class ProductVariant : Entity<ProductVariantId>
{
    public Sku Sku { get; private set; }
    public Money Price { get; private set; } = default!;
    public Weight Weight { get; private set; } = default!;
    public Dimensions Dimensions { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private readonly List<VariantAttribute> _attributes = [];
    public IReadOnlyCollection<VariantAttribute> Attributes => _attributes.AsReadOnly();
    
    private ProductVariant() { }

    public static Result<ProductVariant> Create(Sku sku, Money price, Weight weight, Dimensions dimensions)
    {
        if (sku is null)
            return Result<ProductVariant>.Failure(ProductVariantError.EmptySku);

        var priceResult = price.IsLessThanOrEqualTo(Money.Zero(price.Currency));

        if (priceResult.Value)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidPrice);

        var weightValidation = weight.IsLessThanOrEqual(Weight.Zero(weight.Unit));
        if (weightValidation.IsFailure)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidWeight);

        if (weightValidation.Value)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidWeight);
        
        if (dimensions.IsZero)
            return Result<ProductVariant>.Failure(ProductVariantError.InvalidDimensions);

        var variant = new ProductVariant
        {
            Id = ProductVariantId.New(),
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
            return Result.Failure(ProductVariantError.DuplicateAttributeName);

        _attributes.Add(attributeResult.Value!);

        return Result.Success();
    }

    public Result RemoveAttribute(string name)
    {
        var attribute = _attributes
            .FirstOrDefault(x => x.Name.Equals(name.Trim(), StringComparison.InvariantCultureIgnoreCase));
        
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

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}