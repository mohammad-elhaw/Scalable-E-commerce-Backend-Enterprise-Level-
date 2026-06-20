using Inventory.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence.Repositories;

public class WarehouseRepository(InventoryDbContext context)
    : IWarehouseRepository
{
    public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
        => await context.Warehouses.AddAsync(warehouse, cancellationToken);

    public async Task<Warehouse?> GetByCodeAsync(string code, CancellationToken cancellationToken)
        => await context.Warehouses
        .SingleOrDefaultAsync(
            w => w.WarehouseContent.Code == code
            && !w.IsDeleted, cancellationToken);

    public async Task<Warehouse?> GetByIdAsync(WarehouseId warehouseId, CancellationToken cancellationToken)
        => await context.Warehouses.SingleOrDefaultAsync(
            w => w.Id == warehouseId
            && !w.IsDeleted, cancellationToken);

    public void Remove(Warehouse warehouse)
        => context.Warehouses.Remove(warehouse);
}