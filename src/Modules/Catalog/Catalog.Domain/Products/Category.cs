using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class Category : Entity<CategoryId>
{
    public CategoryContent CategoryContent { get; private set; }
    public CategoryId? ParentId { get; private set; }
    public SeoMetadata SeoMetadata { get; private set; }

    private Category()
    {
    }

    public static Result<Category> Create(
        CategoryContent content,
        SeoMetadata seoMetadata)
    {
        
        var category = new Category
        {
            Id = CategoryId.New(),
            CategoryContent = content,
            SeoMetadata = seoMetadata
        };
        
        //category.RaiseDomainEvent(new CategoryCreatedDomainEvent(category.Id));

        return Result<Category>.Success(category);
    }

    public Result SetParent(Category category)
    {
        if (category.Id == Id)
            return Result.Failure(CategoryErrors.InvalidParentCategory);

        ParentId = category.Id;
        //RaiseDomainEvent(new CategoryParentChangedDomainEvent(Id, category.Id));

        return Result.Success();
    }

}
