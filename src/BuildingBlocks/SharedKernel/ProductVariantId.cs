namespace SharedKernel;

public sealed record ProductVariantId(Guid Value)
{
    public static ProductVariantId New() => new(Guid.NewGuid());
}
