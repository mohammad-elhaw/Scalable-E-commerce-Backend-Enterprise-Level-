using SharedKernel;

namespace Inventory.Domain.Warehouses;

public class Warehouse : AuditableAggregateRoot<WarehouseId>
{
    public WarehouseContent WarehouseContent { get; private set; }
    public WarehouseAddress Address { get; private set; }
    public bool IsActive { get; private set; }

    private Warehouse() { }

    public static Result<Warehouse> Create(
        WarehouseContent content,
        WarehouseAddress address)
    {
        var warehouse = new Warehouse
        {
            Id = WarehouseId.New(),
            WarehouseContent = content,
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