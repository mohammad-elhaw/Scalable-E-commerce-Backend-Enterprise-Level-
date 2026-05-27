using Infrastructure;
using Inventory.Infrastructure;

namespace Ecommerce.Gateway.Extensions;

public static class InitializeApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration config)
    {

        services.AddInfrastructure();
        services.AddInventoryInfrastructure(config);

        services.AddAuthentication();
        services.AddAuthorization();
        return services;
    }
}
