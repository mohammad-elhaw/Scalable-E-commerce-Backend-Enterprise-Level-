using Inventory.Domain.Warehouses;

namespace Inventory.Domain.Reservations;

public interface IWarehouseRepository
{
    Task<Warehouse?> GetByIdAsync(WarehouseId warehouseId, 
        CancellationToken cancellationToken);

    Task<Warehouse?> GetByCodeAsync(string code,
        CancellationToken cancellationToken);

    Task AddAsync(Warehouse warehouse,
        CancellationToken cancellationToken);
    
    void Remove(Warehouse warehouse);
}