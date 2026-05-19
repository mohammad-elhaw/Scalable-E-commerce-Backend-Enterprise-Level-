namespace Catalog.Domain.Categories;

public sealed record CategoryId(Guid Value)
{
    public static CategoryId New() => new(Guid.NewGuid());
}