namespace Catalog.Domain.Brands;

public sealed record BrandId(Guid Value)
{
    public static BrandId New() => new(Guid.NewGuid());
}