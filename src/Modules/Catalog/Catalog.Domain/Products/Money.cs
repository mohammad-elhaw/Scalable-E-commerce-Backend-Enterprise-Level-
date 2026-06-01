using Catalog.Domain.Products.Errors;
using SharedKernel;

namespace Catalog.Domain.Products;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }
    public static Money Zero(Currency currency) => new (0, currency);

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            return Result<Money>.Failure(MoneyErrors.NegativeAmount);

        if (currency is null || string.IsNullOrWhiteSpace(currency.Code))
            return Result<Money>.Failure(
                MoneyErrors.InvalidCurrency);

        var normalizedCurrency =
            new Currency(currency.Code.Trim().ToUpperInvariant());

        var roundedAmount = decimal.Round(
            amount,
            2,
            MidpointRounding.AwayFromZero);

        return Result<Money>.Success(
            new Money(
                roundedAmount,
                normalizedCurrency));
    }

    private static Result EnsureSameCurrency(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            return Result.Failure(MoneyErrors.CurrencyMismatch);

        return Result.Success();
    }

    public Result<bool> IsGreaterThan(Money other)
    {
        var currencyCheck = EnsureSameCurrency(this, other);

        if(currencyCheck.IsFailure)
            return Result<bool>.Failure(currencyCheck.Error);

        return Result<bool>.Success(Amount > other.Amount);
    }

    public Result<bool> IsLessThan(Money other)
    {
        var currencyCheck = EnsureSameCurrency(this, other);

        if(currencyCheck.IsFailure)
            return Result<bool>.Failure(currencyCheck.Error);

        return Result<bool>.Success(Amount < other.Amount);
    }

    public Result<bool> IsEqualTo(Money other)
    {
        var currencyCheck = EnsureSameCurrency(this, other);
        
        if(currencyCheck.IsFailure)
            return Result<bool>.Failure(currencyCheck.Error);

        return Result<bool>.Success(Amount == other.Amount);
    }

    public Result<bool> IsLessThanOrEqualTo(Money other)
    {
        var currencyCheck = EnsureSameCurrency(this, other);

        if (currencyCheck.IsFailure)
            return Result<bool>.Failure(currencyCheck.Error);

        return Result<bool>.Success(Amount <= other.Amount);
    }

    public Result<Money> Add(Money other)
    {
        var currencyCheck = EnsureSameCurrency(this, other);

        if (currencyCheck.IsFailure)
            return Result<Money>.Failure(currencyCheck.Error);

        return Create(Amount + other.Amount, Currency);
    }

    public Result<Money> Subtract(Money other)
    {
        var currencyCheck = EnsureSameCurrency(this, other);

        if (currencyCheck.IsFailure)
            return Result<Money>.Failure(currencyCheck.Error);

        return Create(Amount - other.Amount, Currency);
    }

    public Result<Money> Multiply(decimal factor)
    {
        if (factor < 0)
            return Result<Money>.Failure(MoneyErrors.NegativeAmount);

        return Create(Amount * factor, Currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString()
        => $"{Amount:0.00} {Currency}";
}
