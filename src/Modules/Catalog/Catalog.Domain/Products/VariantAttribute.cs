using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class VariantAttribute
    : ValueObject
{
    public string Name { get; }
    public string Value { get; }

    private VariantAttribute(string name, string value) 
    {
        Name = name.Trim().ToLowerInvariant();
        Value = value.Trim();
    }

    public static Result<VariantAttribute> Create(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<VariantAttribute>.Failure(ProductVariantError.EmptyAttributeName);

        if (string.IsNullOrWhiteSpace(value))
            return Result<VariantAttribute>.Failure(ProductVariantError.EmptyAttributeValue);
        
        return Result<VariantAttribute>.Success(new VariantAttribute(name, value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}
