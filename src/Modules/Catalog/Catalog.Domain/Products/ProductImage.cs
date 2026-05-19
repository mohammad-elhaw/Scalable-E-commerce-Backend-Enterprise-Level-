using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class ProductImage : Entity<int>
{
    public string ImageUrl { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public int SortOrder { get; private set; }

    private ProductImage()
    {
    }

    internal ProductImage(string imageUrl, bool isPrimary, int sortOrder)
    {
        ImageUrl = imageUrl;
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
    }

}
