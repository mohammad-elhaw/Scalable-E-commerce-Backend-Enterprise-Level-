using Inventory.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

internal class WarehouseConfiguration
    : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                value => new WarehouseId(value));

        builder.OwnsOne(x => x.WarehouseContent, wc =>
        {
            wc.Property(w => w.Name).HasColumnName("Name")
                .HasMaxLength(100);
            wc.Property(w => w.Code).HasColumnName("Code")
                .HasMaxLength(50);

            wc.HasIndex(w => w.Code).IsUnique();
        });

        builder.OwnsOne(x => x.Address, wa =>
        {
            wa.Property(a => a.Country).HasColumnName("Country")
                .HasMaxLength(100);
            wa.Property(a => a.City).HasColumnName("City")
                .HasMaxLength(100);
            wa.Property(a => a.State).HasColumnName("State")
                .HasMaxLength(100);
            wa.Property(a => a.PostalCode).HasColumnName("PostalCode")
                .HasMaxLength(20);
            wa.Property(a => a.AddressLine).HasColumnName("AddressLine")
                .HasMaxLength(200);
        });

    }
}