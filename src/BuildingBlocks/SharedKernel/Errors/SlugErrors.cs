namespace SharedKernel.Errors;

public static class SlugErrors
{
    public static readonly Error EmptySlug = new(
        "Slug.Empty",
        "Slug cannot be empty.",
        default);

    public static readonly Error SlugTooLong = new(
        "Slug.TooLong",
        "Slug cannot be longer than 100 characters.",
        default);
}
