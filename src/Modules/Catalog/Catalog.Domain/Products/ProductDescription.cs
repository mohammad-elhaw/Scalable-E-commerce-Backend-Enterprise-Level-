using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class ProductDescription : ValueObject
{
    public string Value { get; }

    private ProductDescription(string value)
    {
        Value = value;
    }

    public static Result<ProductDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<ProductDescription>.Failure(ProductErrors.EmptyDescription);

        if (value.Length > 1000)
            return Result<ProductDescription>.Failure(ProductErrors.DescriptionTooLong);
        
        return Result<ProductDescription>.Success(new ProductDescription(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
