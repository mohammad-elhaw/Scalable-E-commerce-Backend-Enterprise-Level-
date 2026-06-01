using Catalog.Domain.Products.Errors;
using SharedKernel;
using SharedKernel.Errors;

namespace Catalog.Domain.Products;
public sealed class ProductContent : ValueObject
{
    public string Name { get; }
    public string Slug { get; }
    public string Description { get; }

    private ProductContent(
        string name,
        string slug,
        string description)
    {
        Name = name;
        Slug = slug;
        Description = description;
    }

    public static Result<ProductContent> Create(
        string name,
        string slug,
        string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<ProductContent>.Failure(ProductErrors.EmptyName);

        if (name.Length > 100)
            return Result<ProductContent>.Failure(ProductErrors.NameTooLong);

        if (string.IsNullOrWhiteSpace(slug))
            return Result<ProductContent>.Failure(SlugErrors.EmptySlug);

        if (slug.Length > 100)
            return Result<ProductContent>.Failure(SlugErrors.SlugTooLong);

        var normalized = slug.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(description))
            return Result<ProductContent>.Failure(ProductErrors.EmptyDescription);

        if (description.Length > 1000)
            return Result<ProductContent>.Failure(ProductErrors.DescriptionTooLong);

        return Result<ProductContent>.Success(new ProductContent(name.Trim(), normalized, description));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Slug;
        yield return Description;
    }
}