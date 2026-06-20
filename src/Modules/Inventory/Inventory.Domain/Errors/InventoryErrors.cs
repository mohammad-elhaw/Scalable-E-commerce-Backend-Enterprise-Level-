using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Inventory.Domain.Errors;

public static class InventoryErrors
{
    public static readonly Error InvalidQuantity =
        new(
            "Inventory.InvalidQuantity",
            "Quantity must be greater than zero.",
            StatusCodes.Status400BadRequest);

    public static readonly Error InsufficientStock =
        new(
            "Inventory.InsufficientStock",
            "Insufficient stock available.",
            StatusCodes.Status409Conflict);

    public static readonly Error ReservationExceedsAvailableStock =
        new(
            "Inventory.ReservationExceedsAvailableStock",
            "Reservation exceeds available stock.",
            StatusCodes.Status409Conflict);

    public static readonly Error InvalidAdjustment =
        new(
            "Inventory.InvalidAdjustment",
            "Inventory adjustment is invalid.",
            StatusCodes.Status400BadRequest);

    public static readonly Error InventoryItemInactive =
        new(
            "Inventory.InventoryItemInactive",
            "Inventory item is inactive.",
            StatusCodes.Status409Conflict);

    public static readonly Error InvalidWarehouse =
        new(
            "Inventory.InvalidWarehouse",
            "Warehouse is invalid.",
            StatusCodes.Status404NotFound);

    public static readonly Error DuplicateSkuInWarehouse =
        new(
            "Inventory.DuplicateSkuInWarehouse",
            "SKU already exists in warehouse.",
            StatusCodes.Status409Conflict);

    public static readonly Error InvalidExpirationTime =
        new(
            "Inventory.InvalidExpirationTime",
            "Expiration time must be in the future.",
            StatusCodes.Status400BadRequest);

    public static readonly Error AdjustmentLessThanReserved =
        new(
            "Inventory.AdjustmentLessThanReserved",
            "Adjustment can't be less than reserved inventory.",
            StatusCodes.Status409Conflict);

    public static readonly Error InventoryItemNotFound =
        new(
            "Inventory.InventoryItemNotFound",
            "Inventory item not found.",
            StatusCodes.Status404NotFound);

    public static readonly Error InvalidProductVariantId =
        new(
            "Inventory.InvalidProductVariantId",
            "Product variant ID is invalid.",
            StatusCodes.Status400BadRequest);

    public static readonly Error InvalidWarehouseId =
        new(
            "Inventory.InvalidWarehouseId",
            "Warehouse ID is invalid.",
            StatusCodes.Status400BadRequest);
}
