using Inventory.Domain.Errors;
using Inventory.Domain.InventoryItems;
using SharedKernel;

namespace Inventory.Domain.Reservations;

public class InventoryReservation : 
    AuditableAggregateRoot<ReservationId>
{
    public InventoryItemId InventoryItemId { get; private set; }
    public Guid OrderId { get; private set; }
    public ReservationQuantity Quantity { get; private set; }
    public ReservationStatus Status { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;

    private InventoryReservation() { }

    public static Result<InventoryReservation> Create(
        InventoryItemId inventoryItemId,
        Guid orderId,
        ReservationQuantity quantity,
        DateTime expiresAtUtc)
    {
        if (expiresAtUtc <= DateTime.UtcNow)
            return Result<InventoryReservation>.Failure(InventoryErrors.InvalidExpirationTime);

        var reservation = new InventoryReservation
        {
            Id = ReservationId.New(),
            InventoryItemId = inventoryItemId,
            OrderId = orderId,
            Quantity = quantity,
            Status = ReservationStatus.Active,
            ExpiresAtUtc = expiresAtUtc
        };
        return Result<InventoryReservation>.Success(reservation);
    }

    public Result Confirm()
    {
        if (Status != ReservationStatus.Active)
            return Result.Failure(ReservationErrors.InvalidState);

        Status = ReservationStatus.Confirmed;
        return Result.Success();
    }
    public Result Expire()
    {
        if(Status != ReservationStatus.Active)
            return Result.Failure(ReservationErrors.InvalidState);

        if(DateTime.UtcNow < ExpiresAtUtc)
            return Result.Failure(ReservationErrors.NotExpiredYet);

        Status = ReservationStatus.Expired;

        RaiseDomainEvent(
            new ReservationExpiredDomainEvent(
                Id, 
                InventoryItemId, 
                Quantity.Value));

        return Result.Success();
    }

    public void Cancel()
        => Status = ReservationStatus.Cancelled;
    
}