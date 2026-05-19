using SharedKernel.Errors;

namespace SharedKernel;

public sealed class Slug : ValueObject
{
    public string Value { get; }

    private Slug(string value)
    {
        Value = value.Trim().ToLowerInvariant();
    }

    public static Result<Slug> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Slug>.Failure(SlugErrors.EmptySlug);
        if (value.Length > 100)
            return Result<Slug>.Failure(SlugErrors.SlugTooLong);

        var normalized = value.Trim().ToLowerInvariant();

        return Result<Slug>.Success(new Slug(normalized));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
