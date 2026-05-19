using Catalog.Domain.Categories.Errors;
using Catalog.Domain.Products;
using SharedKernel;

namespace Catalog.Domain.Categories;

public sealed class Category
    : AuditableAggregateRoot<CategoryId>
{
    public CategoryName Name { get; private set; }
    public Slug Slug { get; private set; }
    public CategoryId? ParentId { get; private set; }
    public CategoryDescription? Description { get; private set; }
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
            Name = content.Name,
            Slug = content.Slug,
            Description = content.Description,
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
