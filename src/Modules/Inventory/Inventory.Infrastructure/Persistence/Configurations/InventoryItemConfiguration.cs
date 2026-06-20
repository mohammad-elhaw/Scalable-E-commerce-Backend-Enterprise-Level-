using Inventory.Domain.InventoryItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

internal class InventoryItemConfiguration
    : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItems");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new InventoryItemId(value))
            .IsRequired();

        builder.Property(x => x.ProductVariantId)
            .HasConversion(
                id => id.Value,
                value => new SharedKernel.ProductVariantId(value));

        builder.Property(x => x.WarehouseId)
            .HasConversion(
                id => id.Value,
                value => new Domain.Warehouses.WarehouseId(value));

        builder.Property(x => x.QuantityOnHand)
            .HasConversion(
               q => q.Value,
                value => StockQuantity.Create(value).Value!)
            .HasColumnName("QuantityOnHand");

        builder.Property(x => x.ReservedQuantity)
            .HasConversion(
                q => q.Value,
                value => StockQuantity.Create(value).Value!)
            .HasColumnName("ReservedQuantity");

        builder.HasMany<InventoryTransaction>(x => x.Transactions)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Transactions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
