using Inventory.Domain.InventoryItems;
using Inventory.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

internal class InventoryReservationConfiguration
    : IEntityTypeConfiguration<InventoryReservation>
{
    public void Configure(EntityTypeBuilder<InventoryReservation> builder)
    {
        builder.ToTable("InventoryReservations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new ReservationId(value));

        builder.Property(x => x.InventoryItemId)
            .HasConversion(
                id => id.Value,
                value => new InventoryItemId(value))
            .IsRequired();

        builder.Property(x => x.OrderId)
            .IsRequired();

        builder.Property(x => x.ReservationQuantity)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .IsRequired();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.InventoryItemId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.ExpiresAtUtc);
    }
}
