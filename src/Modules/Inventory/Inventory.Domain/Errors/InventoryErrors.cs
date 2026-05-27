using SharedKernel;

namespace Inventory.Domain.Errors;

public static class InventoryErrors
{
    public static readonly Error InvalidQuantity =
        new(
            "Inventory.InvalidQuantity",
            "Quantity must be greater than zero.",
            default);

    public static readonly Error InsufficientStock =
        new(
            "Inventory.InsufficientStock",
            "Insufficient stock available.",
            default);

    public static readonly Error ReservationExceedsAvailableStock =
        new(
            "Inventory.ReservationExceedsAvailableStock",
            "Reservation exceeds available stock.",
            default);

    public static readonly Error InvalidAdjustment =
        new(
            "Inventory.InvalidAdjustment",
            "Inventory adjustment is invalid.",
            default);

    public static readonly Error InventoryItemInactive =
        new(
            "Inventory.InventoryItemInactive",
            "Inventory item is inactive.",
            default);

    public static readonly Error InvalidWarehouse =
        new(
            "Inventory.InvalidWarehouse",
            "Warehouse is invalid.",
            default);

    public static readonly Error DuplicateSkuInWarehouse =
        new(
            "Inventory.DuplicateSkuInWarehouse",
            "SKU already exists in warehouse.",
            default);

    public static readonly Error InvalidExpirationTime =
        new(
            "Inventory.InvalidExpirationTime",
            "Expiration time must be in the future.",
            default);

    public static readonly Error AdjustmentLessThanReserved =
        new(
            "Inventory.AdjustmentLessThanReserved",
            "Adjustment can't be less than reserved inventory.",
            default);
}
