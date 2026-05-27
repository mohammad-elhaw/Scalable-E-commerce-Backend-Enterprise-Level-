using SharedKernel;

namespace Inventory.Domain.Warehouses;

public class Warehouse : AuditableAggregateRoot<WarehouseId>
{
    public WarehouseName Name { get; private set; }
    public WarehouseCode Code { get; private set; }
    public WarehouseAddress Address { get; private set; }
    public bool IsActive { get; private set; }

    private Warehouse() { }

    public static Result<Warehouse> Create(
        WarehouseName name,
        WarehouseCode code,
        WarehouseAddress address)
    {
        var warehouse = new Warehouse
        {
            Id = WarehouseId.New(),
            Name = name,
            Code = code,
            Address = address,
            IsActive = true
        };

        return Result<Warehouse>.Success(warehouse);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void ChangeAddress(WarehouseAddress address)
    {
        Address = address;
    }
}