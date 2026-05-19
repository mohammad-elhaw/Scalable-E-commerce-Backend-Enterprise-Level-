using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class ProductName : ValueObject
{
    public string Value { get; }

    private ProductName(string value)
    {
        Value = value;
    }

    public static Result<ProductName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<ProductName>.Failure(ProductErrors.EmptyName);

        if (value.Length > 100)
            return Result<ProductName>.Failure(ProductErrors.NameTooLong);

        return Result<ProductName>.Success(new ProductName(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ProductName name) => name.Value;
}