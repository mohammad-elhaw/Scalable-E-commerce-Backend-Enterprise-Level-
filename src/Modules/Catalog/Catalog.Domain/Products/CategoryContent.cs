using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class CategoryContent : ValueObject
{
    public string Name { get; }
    public string Slug { get; }
    public string? Description { get; }
    private CategoryContent(
        string name,
        string slug,
        string? description)
    {
        Name = name;
        Slug = slug;
        Description = description;
    }
    public static Result<CategoryContent> Create(
        string name,
        string slug,
        string? description)
    {
        if (string.IsNullOrEmpty(name))
            return Result<CategoryContent>.Failure(CategoryErrors.EmptyName);

        if (name.Length > 100)
            return Result<CategoryContent>.Failure(CategoryErrors.NameTooLong);

        if (string.IsNullOrWhiteSpace(slug))
            return Result<CategoryContent>.Failure(CategoryErrors.EmptySlug);

        if (slug.Length > 100)
            return Result<CategoryContent>.Failure(CategoryErrors.SlugTooLong);

        var normalized = slug.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(description))
            return Result<CategoryContent>.Failure(CategoryErrors.EmptyDescription);

        if (description.Length > 1000)
            return Result<CategoryContent>.Failure(CategoryErrors.DescriptionTooLong);


        return Result<CategoryContent>.Success(new CategoryContent(name, normalized, description));
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Slug;

        if (Description is not null)
            yield return Description;
    }
}