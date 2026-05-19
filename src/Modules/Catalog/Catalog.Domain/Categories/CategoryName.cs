using Catalog.Domain.Categories.Errors;
using SharedKernel;

namespace Catalog.Domain.Categories;

public sealed class CategoryName : ValueObject
{
    public string Value { get; }

    private CategoryName(string name) 
    {  
        Value = name; 
    }

    public static Result<CategoryName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result<CategoryName>.Failure(CategoryErrors.EmptyName);

        if (value.Length > 100)
            return Result<CategoryName>.Failure(CategoryErrors.NameTooLong);

        return Result<CategoryName>.Success(new CategoryName(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(CategoryName name) => name.Value;
}
