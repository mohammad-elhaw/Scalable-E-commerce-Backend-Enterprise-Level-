using System.Data;

namespace Inventory.Queries;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}