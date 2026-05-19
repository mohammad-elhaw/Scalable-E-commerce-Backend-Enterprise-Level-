namespace Catalog.Domain.Products;

public sealed record Currency(string Code)
{
    public static readonly Currency USD = new("USD");
    public static readonly Currency EUR = new("EUR");
    public static readonly Currency GBP = new("GBP");
    public static readonly Currency EGP = new("EGP");
}
