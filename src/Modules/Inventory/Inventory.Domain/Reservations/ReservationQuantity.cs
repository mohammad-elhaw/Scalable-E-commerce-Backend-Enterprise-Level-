using Inventory.Domain.Errors;
using SharedKernel;

namespace Inventory.Domain.Reservations;

public sealed class ReservationQuantity : ValueObject
{
    public int Value { get; }

    private ReservationQuantity(int value)
    {
        Value = value;
    }

    public static Result<ReservationQuantity> Create(int value)
    {
        if (value < 0)
            return Result<ReservationQuantity>.Failure(InventoryErrors.InvalidQuantity);

        return Result<ReservationQuantity>.Success(new ReservationQuantity(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}
