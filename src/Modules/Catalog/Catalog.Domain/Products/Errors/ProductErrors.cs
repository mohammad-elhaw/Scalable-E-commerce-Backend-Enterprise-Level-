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
}