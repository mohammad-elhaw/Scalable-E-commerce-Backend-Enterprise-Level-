using SharedKernel;
using SharedKernel.Errors;

namespace Catalog.Domain.Products;

public sealed class SeoMetadata : ValueObject
{
    public string MetaTitle { get; }
    public string MetaDescription { get; }
    public string? CanonicalUrl { get; }


    private SeoMetadata(string metaTitle, string metaDescription, string? canonicalUrl)
    {
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        CanonicalUrl = canonicalUrl;
    }

    public static Result<SeoMetadata> Create(string metaTitle, string metaDescription, string? canonicalUrl)
    {
        if (string.IsNullOrWhiteSpace(metaTitle))
            return Result<SeoMetadata>.Failure(SeoMetaDataErrors.EmptyMetaTitle);

        if (metaTitle.Length > 60)
            return Result<SeoMetadata>.Failure(SeoMetaDataErrors.MetaTitleTooLong);
        
        if (string.IsNullOrWhiteSpace(metaDescription))
            return Result<SeoMetadata>.Failure(SeoMetaDataErrors.EmptyMetaDescription);
        
        if (metaDescription.Length > 160)
            return Result<SeoMetadata>.Failure(SeoMetaDataErrors.MetaDescriptionTooLong);
        
        if (canonicalUrl is not null && canonicalUrl.Length > 2000)
            return Result<SeoMetadata>.Failure(SeoMetaDataErrors.CanonicalUrlTooLong);

        return Result<SeoMetadata>.Success(new SeoMetadata(metaTitle, metaDescription, canonicalUrl));
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return MetaTitle;
        yield return MetaDescription;
        
        if(CanonicalUrl is not null)
            yield return CanonicalUrl;
    }
}
