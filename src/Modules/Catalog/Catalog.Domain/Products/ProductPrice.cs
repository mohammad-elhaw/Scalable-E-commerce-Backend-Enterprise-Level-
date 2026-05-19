using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class ProductPrice : ValueObject
{
    public Money BasePrice { get; }
    public Money? CompareAtPrice { get; }

    private ProductPrice(Money basePrice, Money? compareAtPrice)
    {
        BasePrice = basePrice;
        CompareAtPrice = compareAtPrice;
    }

    public static Result<ProductPrice> Create(Money basePrice, Money? compareAtPrice)
    {
        var basePriceValidation = basePrice.IsLessThan(Money.Zero(basePrice.Currency));

        if(basePriceValidation.IsFailure)
            return Result<ProductPrice>.Failure(basePriceValidation.Error);

        if(basePriceValidation.Value)
            return Result<ProductPrice>.Failure(ProductPriceError.InvalidBasePrice);

        if(compareAtPrice is not null)
        {
            var compareCheck = compareAtPrice
                .IsLessThanOrEqualTo(Money.Zero(compareAtPrice.Currency));
        
            if(compareCheck.IsFailure)
                return Result<ProductPrice>.Failure(compareCheck.Error);

            if(compareCheck.Value)
                return Result<ProductPrice>.Failure(ProductPriceError.InvalidCompareAtPrice);

            var comparision = compareAtPrice.IsLessThanOrEqualTo(basePrice);

            if(comparision.IsFailure)
                return Result<ProductPrice>.Failure(comparision.Error);

            if(comparision.Value)
                return Result<ProductPrice>
                    .Failure(ProductPriceError.CompareAtPriceMustBeGreaterThanBasePrice);
        }

        return Result<ProductPrice>.Success(new ProductPrice(basePrice, compareAtPrice));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return BasePrice;
        if (CompareAtPrice is not null)
            yield return CompareAtPrice;
    }
}
