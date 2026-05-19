using SharedKernel;

namespace Catalog.Domain.Products.Errors;

internal static class ProductPriceError
{
    internal static readonly Error InvalidBasePrice = 
        new(
            "ProductPrice.InvalidBasePrice",
            "Product price must be greater than zero.",
            default);

    
    internal static readonly Error InvalidCompareAtPrice =
        new(
            "ProductPrice.InvalidCompareAtPrice",
            "Compare at price must be greater than zero.",
            default);

    internal static readonly Error CompareAtPriceMustBeGreaterThanBasePrice =
        new(
            "ProductPrice.CompareAtPriceMustBeGreaterThanBasePrice",
            "Compare at price must be greater than base price.",
            default);
}
