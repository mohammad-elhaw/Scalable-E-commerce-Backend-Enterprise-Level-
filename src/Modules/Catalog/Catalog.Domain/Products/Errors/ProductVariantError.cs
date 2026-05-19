using SharedKernel;

namespace Catalog.Domain.Products.Errors;

internal static class ProductVariantError
{
    internal static readonly Error EmptySku =
        new(
            "ProductVariant.EmptySku",
            "Product variant SKU cannot be empty.",
            default);

    internal static readonly Error InvalidPrice =
        new(
            "ProductVariant.InvalidPrice",
            "Product variant price must be greater than zero.",
            default);

    internal static readonly Error InvalidWeight =
        new(
            "ProductVariant.InvalidWeight",
            "Product variant weight must be greater than zero.",
            default);

    internal static readonly Error InvalidDimensions =
        new(
            "ProductVariant.InvalidDimensions",
            "Product variant dimensions must be greater than zero.",
            default);

    internal static readonly Error EmptyAttributeName =
        new(
            "ProductVariant.EmptyAttributeName",
            "Variant attribute name cannot be empty.",
            default);
    
    internal static readonly Error EmptyAttributeValue =
        new(
            "ProductVariant.EmptyAttributeValue",
            "Variant attribute value cannot be empty.",
            default);
    
    internal static readonly Error InvalidWeightUnit =
        new(
            "ProductVariant.InvalidWeightUnit",
            "Invalid weight unit.",
            default);

    internal static readonly Error AttributeNotFound = 
        new(
            "ProductVariant.AttributeNotFound",
            "Variant attribute not found.",
            default);
}
