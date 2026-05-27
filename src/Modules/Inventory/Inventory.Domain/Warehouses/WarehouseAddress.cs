using Inventory.Domain.Errors;
using SharedKernel;

namespace Inventory.Domain.Warehouses;

public sealed class WarehouseAddress : ValueObject
{
    public string Country { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string AddressLine { get; }

    private WarehouseAddress(
        string country,
        string city,
        string state,
        string postalCode,
        string addressLine)
    {
        Country = country;
        City = city;
        State = state;
        PostalCode = postalCode;
        AddressLine = addressLine;
    }

    public static Result<WarehouseAddress> Create(
        string country,
        string city,
        string state,
        string postalCode,
        string addressLine)
    {
        if (string.IsNullOrWhiteSpace(country) ||
            string.IsNullOrWhiteSpace(city) ||
            string.IsNullOrWhiteSpace(state) ||
            string.IsNullOrWhiteSpace(postalCode) ||
            string.IsNullOrWhiteSpace(addressLine))
        {
            return Result<WarehouseAddress>.Failure(InventoryErrors.InvalidWarehouse);
        }
        return Result<WarehouseAddress>.Success(new WarehouseAddress(
            country.Trim(),
            city.Trim(),
            state.Trim(),
            postalCode.Trim(),
            addressLine.Trim()));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return City;
        yield return State; 
        yield return PostalCode;
        yield return AddressLine;
    }
}
