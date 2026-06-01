using SharedKernel;

namespace Catalog.Domain.Products.Errors;

public static class MoneyErrors
{
    public static readonly Error NegativeAmount =
        new(
            "Money.NegativeAmount",
            "Money amount cannot be negative.",
            default);

    public static readonly Error InvalidCurrency =
        new(
            "Money.InvalidCurrency",
            "Currency is invalid.",
            default);

    public static readonly Error CurrencyMismatch =
        new(
            "Money.CurrencyMismatch",
            "Currencies must match.",
            default);
}
