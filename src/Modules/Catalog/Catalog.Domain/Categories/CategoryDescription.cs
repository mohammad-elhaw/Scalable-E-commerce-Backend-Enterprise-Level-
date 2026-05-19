using Catalog.Domain.Categories.Errors;
using SharedKernel;

namespace Catalog.Domain.Categories;

public sealed class CategoryDescription
    : ValueObject
{
    public string Value { get; }

    private CategoryDescription(string value)
    {
        Value = value; 
    }

    public static Result<CategoryDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<CategoryDescription>.Failure(CategoryErrors.EmptyDescription);

        if (value.Length > 1000)
            return Result<CategoryDescription>.Failure(CategoryErrors.DescriptionTooLong);

        return Result<CategoryDescription>.Success(new CategoryDescription(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(CategoryDescription description)
        => description.Value;
}
