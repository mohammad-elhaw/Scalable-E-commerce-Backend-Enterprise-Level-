using Catalog.Domain.Brands;
using Catalog.Domain.Categories;
using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public class Product : AuditableAggregateRoot<ProductId>
{
    private readonly List<ProductVariant> _variants = [];

    private readonly List<ProductImage> _images = [];

    private readonly List<Category> _categories = [];

    public ProductName Name { get; private set; }
    public Slug Slug { get; private set; }
    public ProductDescription Description { get; private set; }
    public ProductPrice Price { get; private set; }
    public BrandId? BrandId { get; private set; }
    public SeoMetadata SeoMetadata { get; private set; }
    public ProductStatus Status { get; private set; }
    public bool IsPublished { get; private set; }
    public DateTime? PublishedAtUtc { get; private set; }

    public IReadOnlyCollection<ProductVariant> Variants
        => _variants;

    public IReadOnlyCollection<ProductImage> Images
        => _images;

    public IReadOnlyCollection<Category> Categories
        => _categories;

    private Product()
    {
    }

    public static Result<Product> Create(
        ProductContent content,
        ProductPrice price,
        BrandId? brandId,
        SeoMetadata seoMetadata,
        ProductStatus status)
    {
        var product = new Product
        {
            Id = ProductId.New(),
            Name = content.Name,
            Slug = content.Slug,
            Description = content.Description,
            Price = price,
            BrandId = brandId,
            SeoMetadata = seoMetadata,
            Status = status
        };

        //product.RaiseDomainEvent(
        //    new ProductCreatedDomainEvent(product.Id));

        return Result<Product>.Success(product);
    }

    public Result Publish()
    {
        var validationResult = EnsureCanPublish();
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        IsPublished = true;
        PublishedAtUtc = DateTime.UtcNow;

        //RaiseDomainEvent(
        //    new ProductPublishedDomainEvent(Id));

        return Result.Success();
    }

    private Result EnsureCanPublish()
    {
        if (!_variants.Any())
            return Result.Failure(ProductErrors.NoVariants);

        if(!_images.Any())
            return Result.Failure(ProductErrors.NoImages);

        if(!_categories.Any())
            return Result.Failure(ProductErrors.NoCategories);

        return Result.Success();
    }

}