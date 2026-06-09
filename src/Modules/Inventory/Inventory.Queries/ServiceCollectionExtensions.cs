using Inventory.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Queries;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryQueries(this IServiceCollection services)
    {
        services.AddScoped<IInventoryQueries, InventoryQueries>();
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        return services;
    }
}