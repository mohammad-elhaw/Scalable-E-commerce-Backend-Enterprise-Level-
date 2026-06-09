using Infrastructure;
using Inventory.Infrastructure;
using Inventory.Queries;
using System.Reflection;

namespace Ecommerce.Gateway.Extensions;

public static class InitializeApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddInventoryModule(config);

        services.AddInfrastructure(Assembly.GetEntryAssembly());

        services.AddAuthentication();
        services.AddAuthorization();
        return services;
    }


    private static IServiceCollection AddInventoryModule(
        this IServiceCollection services,
        IConfiguration config)
    {

        services.AddInventoryInfrastructure(config);
        services.AddInventoryQueries();
        return services;
    }
}
