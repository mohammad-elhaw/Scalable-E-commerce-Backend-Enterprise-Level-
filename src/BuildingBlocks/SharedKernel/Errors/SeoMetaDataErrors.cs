namespace SharedKernel.Errors;

public static class SeoMetaDataErrors
{
    internal static readonly Error EmptyMetaTitle = new(
        "SeoMetaData.EmptyMetaTitle",
        "SEO meta title cannot be empty.",
        default);

    internal static readonly Error MetaTitleTooLong = new(
        "SeoMetaData.MetaTitleTooLong",
        "SEO meta title cannot exceed 60 characters.",
        default);

    internal static readonly Error EmptyMetaDescription = new(
        "SeoMetaData.EmptyMetaDescription",
        "SEO meta description cannot be empty.",
        default);

    internal static readonly Error MetaDescriptionTooLong = new(
        "SeoMetaData.MetaDescriptionTooLong",
        "SEO meta description cannot exceed 160 characters.",
        default);

    internal static readonly Error CanonicalUrlTooLong = new(
        "SeoMetaData.CanonicalUrlTooLong",
        "Canonical URL cannot exceed 2000 characters.",
        default);
}
