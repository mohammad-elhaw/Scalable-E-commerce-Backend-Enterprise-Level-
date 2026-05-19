using SharedKernel;

namespace Catalog.Domain.Errors;

internal static class ProductImageError
{
    internal static readonly Error EmptyImageUrl =
        new(
            "ProductImage.EmptyImageUrl",
            "Image URL cannot be empty",
            null);
}