namespace Catalog.Domain.Products;

public sealed record CategoryId(Guid Value)
{
    public static CategoryId New() => new(Guid.NewGuid());
}