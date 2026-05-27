using Catalog.Domain.Brands;
using Catalog.Domain.Categories;
using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public class Product : AuditableAggregateRoot<ProductId>
{
    private readonly List<ProductVariant> _variants = [];

    private readonly List<ProductImage> _images = [];

    private readonly List<CategoryId> _categoryIds = [];

    public ProductName Name { get; private set; }
    public Slug Slug { get; private set; }
    public ProductDescription Description { get; private set; }
    public BrandId? BrandId { get; private set; }
    public SeoMetadata SeoMetadata { get; private set; }
    public ProductStatus Status { get; private set; }
    public DateTime? PublishedAtUtc { get; private set; }

    public IReadOnlyList<ProductVariant> Variants
        => _variants.AsReadOnly();

    public IReadOnlyList<ProductImage> Images
        => _images.AsReadOnly();

    public IReadOnlyList<CategoryId> CategoryIds
        => _categoryIds.AsReadOnly();

    private Product()
    {
    }

    public static Result<Product> Create(
        ProductContent content,
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
            BrandId = brandId,
            SeoMetadata = seoMetadata,
            Status = status
        };

        //product.RaiseDomainEvent(
        //    new ProductCreatedDomainEvent(product.Id));

        return Result<Product>.Success(product);
    }

    public Result AddVariant(ProductVariant variant)
    {
        if (variant is null)
            return Result.Failure(ProductErrors.NoVariants);

        var skuExists = _variants.Any(x =>
            string.Equals(x.Sku, variant.Sku, StringComparison.OrdinalIgnoreCase));

        if (skuExists)
            return Result.Failure(ProductErrors.DuplicateSku);

        //RaiseDomainEvent(new ProductVariantAddedDomainEvent(Id, variant.Id, variant.Sku));

        _variants.Add(variant);

        return Result.Success();
    }

    public Result RemoveVariant(ProductVariantId variantId)
    {
        var variant = _variants.FirstOrDefault(x => x.Id == variantId);

        if (variant is null)
            return Result.Failure(ProductErrors.VariantNotFound);

        _variants.Remove(variant);

        return Result.Success();
    }

    public Result AddImage(string imageUrl, bool isPrimary, int sortOrder)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return Result.Failure(ProductErrors.InvalidImage);

        if (_images.Any(x => x.SortOrder == sortOrder))
            return Result.Failure(ProductErrors.DuplicateImageSortOrder);

        var isFirstImage = _images.Count == 0;
        var finalIsPrimary = isPrimary || isFirstImage;


        var image = new ProductImage(imageUrl, finalIsPrimary, sortOrder);
        _images.Add(image);

        if(finalIsPrimary) ClearPrimaryImage(image);

        return Result.Success();
    }

    private void ClearPrimaryImage(ProductImage newPrimary)
    {
        foreach (var image in _images)
            image.UnMarkAsPrimary();

        newPrimary.MarkAsPrimary();
    }

    public Result RemoveImage(int imageId)
    {
        var image = _images.FirstOrDefault(x => x.Id == imageId);
        if (image is null)
            return Result.Failure(ProductErrors.ImageNotFound);

        var wasPrimary = image.IsPrimary;

        _images.Remove(image);

        if(wasPrimary && _images.Count > 0)
        {
            var firstImage = _images
                .OrderBy(x => x.SortOrder)
                .First();

            firstImage.MarkAsPrimary();
        }

        return Result.Success();
    }

    public Result AddCategory(CategoryId categoryId)
    {
        if(_categoryIds.Contains(categoryId))
            return Result.Failure(ProductErrors.CategoryAlreadyAssigned);

        _categoryIds.Add(categoryId);
        return Result.Success();
    }

    public Result RemoveCategory(CategoryId categoryId)
    {
        if (!_categoryIds.Contains(categoryId))
            return Result.Failure(ProductErrors.CategoryNotAssigned);

        _categoryIds.Remove(categoryId);
        return Result.Success();
    }

    public Result UpdateSeo(SeoMetadata seoMetadata)
    {
        if (seoMetadata is null)
            return Result.Failure(ProductErrors.InvalidSeoMetadata);

        SeoMetadata = seoMetadata;
        return Result.Success();
    }

    public Result Archive()
    {
        if (Status == ProductStatus.Archived)
            return Result.Success();

        Status = ProductStatus.Archived;

        return Result.Success();
    }

    public Result Publish()
    {
        var validationResult = EnsureCanPublish();
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        Status = ProductStatus.Active;
        PublishedAtUtc = DateTime.UtcNow;

        //RaiseDomainEvent(
        //    new ProductPublishedDomainEvent(Id));

        return Result.Success();
    }

    private Result EnsureCanPublish()
    {
        if (!_variants.Any(v => v.IsActive))
            return Result.Failure(ProductErrors.NoVariants);

        if(_images.Count == 0)
            return Result.Failure(ProductErrors.NoImages);

        if(_categoryIds.Count == 0)
            return Result.Failure(ProductErrors.NoCategories);

        return Result.Success();
    }

}