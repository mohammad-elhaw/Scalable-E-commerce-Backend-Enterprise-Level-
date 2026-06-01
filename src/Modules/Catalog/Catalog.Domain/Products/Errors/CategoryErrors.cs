using SharedKernel;

namespace Catalog.Domain.Products.Errors;

public static class CategoryErrors
{
    public static readonly Error EmptyName = new(
        "Category.EmptyName",
        "Category name can not be empty",
        default);

    public static readonly Error NameTooLong = new(
        "Category.NameTooLong",
        "Category name cannot exceed 100 characters.",
        default);

    public static readonly Error DescriptionTooLong = new(
        "Category.CategoryNameTooLong",
        "Category description cannot exceed 1000 characters.",
        default);

    public static readonly Error EmptyDescription = new(
        "Category.EmptyDesciption",
        "Category description can not be empty",
        default);

    public static readonly Error InvalidParentCategory = new(
        "Category.InvalidParentCategory",
        "Category Parent is invalid",
        default);

    public static readonly Error InvalidChildCategory = new(
        "Category.InvalidChildCategory",
        "Category Child is invalid",
        default);

    public static readonly Error EmptySlug = new(
        "Slug.Empty",
        "Slug cannot be empty.",
        default);

    public static readonly Error SlugTooLong = new(
        "Slug.TooLong",
        "Slug cannot be longer than 100 characters.",
        default);
}
