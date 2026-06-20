using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Domain.Reservations;

public class InventoryReservation :
    AuditableAggregateRoot<ReservationId>
{
    public InventoryItemId InventoryItemId { get; private set; }
    public Guid OrderId { get; private set; }
    public int ReservationQuantity { get; private set; }
    public ReservationStatus Status { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public bool IsExpired(DateTime utcNow) => utcNow >= ExpiresAtUtc;

    private InventoryReservation() { }

    public static Result<InventoryReservation> Create(
        InventoryItemId inventoryItemId,
        Guid orderId,
        int quantity,
        DateTime expiresAtUtc)
    {
        if (expiresAtUtc <= DateTime.UtcNow)
            return Result<InventoryReservation>.Failure(InventoryErrors.InvalidExpirationTime);

        if (quantity < 0)
            return Result<InventoryReservation>.Failure(InventoryErrors.InvalidQuantity);

        var reservation = new InventoryReservation
        {
            Id = ReservationId.New(),
            InventoryItemId = inventoryItemId,
            OrderId = orderId,
            ReservationQuantity = quantity,
            Status = ReservationStatus.Active,
            ExpiresAtUtc = expiresAtUtc
        };

        return Result<InventoryReservation>.Success(reservation);
    }

    public Result Commit()
    {
        if (Status != ReservationStatus.Active)
            return Result.Failure(ReservationErrors.InvalidState);

        Status = ReservationStatus.Committed;
        // raise a domain event for committing the reservation
        return Result.Success();
    }
    public Result Expire()
    {
        if (Status != ReservationStatus.Active)
            return Result.Failure(ReservationErrors.InvalidState);

        if (DateTime.UtcNow < ExpiresAtUtc)
            return Result.Failure(ReservationErrors.NotExpiredYet);

        Status = ReservationStatus.Expired;

        RaiseDomainEvent(
            new ReservationExpiredDomainEvent(
                Id,
                InventoryItemId,
                ReservationQuantity));

        return Result.Success();
    }

    public Result Release()
    {
        if (Status != ReservationStatus.Active)
            return Result.Failure(ReservationErrors.InvalidState);

        Status = ReservationStatus.Released;
        // Raise a domain event for releasing the reservation
        return Result.Success();
    }
    
}