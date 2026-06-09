using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Inventory.Queries;

internal sealed class SqlConnectionFactory(IConfiguration configuration)
        : ISqlConnectionFactory
{
    private readonly string connectionString = configuration.GetConnectionString("Database")!;

    public IDbConnection Create()
    {
        return new SqlConnection(connectionString);
    }
}
