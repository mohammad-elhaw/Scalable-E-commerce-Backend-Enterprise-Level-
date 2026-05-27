using SharedKernel;

namespace Catalog.Domain.Products.Errors;

internal static class ProductErrors
{
    internal static readonly Error EmptyName = new(
        "Product.EmptyName",
        "Product name cannot be empty.",
        default);

    internal static readonly Error NameTooLong = new(
        "Product.NameTooLong",
        "Product name cannot exceed 100 characters.",
        default);

    internal static readonly Error EmptySlug = new(
        "Product.EmptySlug",
        "Product slug cannot be empty.",
        default);

    internal static readonly Error SlugTooLong = new(
        "Product.SlugTooLong",
        "Product slug cannot exceed 100 characters.",
        default);

    internal static readonly Error EmptyDescription = new(
        "Product.EmptyDescription",
        "Product description cannot be empty.",
        default);

    internal static readonly Error DescriptionTooLong = new(
        "Product.DescriptionTooLong",
        "Product description cannot exceed 1000 characters.",
        default);

    internal static readonly Error NoVariants = new(
        "Product.NoVariant",
        "Product must have at least one variant.",
        default);

    internal static readonly Error NoImages = new(
        "Product.NoImages",
        "Product must have at least one image.",
        default);

    internal static readonly Error NoCategories = new(
        "Product.NoCategories",
        "Product must belong to at least one category.",
        default);

    internal static readonly Error DuplicateSku = new(
        "Product.DuplicateSku",
        "product must not contain duplicate variant Sku",
        default);

    internal static readonly Error VariantNotFound = new(
        "Product.VariantNotFound",
        "product variant not found",
        default);

    internal static readonly Error InvalidImage = new(
        "Product.InvalidImage",
        "Image URL cannot be empty.",
        default);

    internal static readonly Error DuplicateImageSortOrder = new(
        "Product.DuplicateImageSortOrder",
        "An image with the same sort order already exists.",
        default);

    internal static readonly Error ImageNotFound = new(
        "Product.ImageNotFound",
        "There is not image with this Id",
        default);

    internal static readonly Error CategoryAlreadyAssigned = new(
        "Product.CategoryAlreadyAssigned",
        "The product is already assigned to this category.",
        default);

    internal static readonly Error CategoryNotAssigned = new(
        "Product.CategoryNotAssigned",
        "The product is not assigned to this category.",
        default);

    internal static readonly Error InvalidSeoMetadata = new(
        "Product.InvalidSeoMetadata",
        "SEO metadata is invalid.",
        default);
}