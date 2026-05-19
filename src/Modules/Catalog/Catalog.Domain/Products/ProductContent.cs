using SharedKernel;

namespace Catalog.Domain.Products;

public sealed record ProductContent(
    ProductName Name,
    Slug Slug,
    ProductDescription Description);
