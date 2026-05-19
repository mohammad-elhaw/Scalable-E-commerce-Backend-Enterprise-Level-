using SharedKernel;

namespace Catalog.Domain.Categories;

public sealed record CategoryContent(
    CategoryName Name,
    Slug Slug,
    CategoryDescription? Description);