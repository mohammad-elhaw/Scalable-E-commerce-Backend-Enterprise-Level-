using Application.Abstractions.Messaging;
using Infrastructure.Persistence;
using Inventory.Domain.InventoryItems;
using Inventory.Domain.Reservations;
using Inventory.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence;

internal class InventoryDbContext(
    DbContextOptions<InventoryDbContext> options, 
    IDomainEventDispatcher dispatcher)
    : ModuleDbContext(options, dispatcher)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<InventoryReservation> InventoryReservations => Set<InventoryReservation>();
}