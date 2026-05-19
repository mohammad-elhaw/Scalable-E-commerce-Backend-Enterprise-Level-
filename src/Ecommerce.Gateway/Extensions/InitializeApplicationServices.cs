using Infrastructure;

namespace Ecommerce.Gateway.Extensions;

public static class InitializeApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {

        services.AddInfrastructure();

        services.AddAuthentication();
        services.AddAuthorization();
        return services;
    }
}
