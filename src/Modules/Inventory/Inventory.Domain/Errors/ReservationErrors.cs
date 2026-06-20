using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Inventory.Domain.Errors;

public static class ReservationErrors
{
    public static readonly Error ReservationNotFound =
        new(
            "Inventory.ReservationNotFound",
            "Reservation not found.",
            StatusCodes.Status404NotFound);

    public static readonly Error ReservationAlreadyExists =
       new(
           "Inventory.ReservationAlreadyExists",
           "A reservation for this product variant and warehouse already exists.",
           StatusCodes.Status409Conflict);

    public static readonly Error InvalidExpirationTime =
        new(
            "Inventory.InvalidExpirationTime",
            "Expiration time must be in the future.",
            StatusCodes.Status400BadRequest);

    public static readonly Error InvalidState =
        new(
            "Inventory.InvalidReservationState",
            "The reservation is not in a valid state for this operation.",
            StatusCodes.Status409Conflict);

    public static readonly Error NotExpiredYet =
        new(
            "Inventory.ReservationNotExpiredYet",
            "The reservation has not expired yet.",
            StatusCodes.Status409Conflict);
}
