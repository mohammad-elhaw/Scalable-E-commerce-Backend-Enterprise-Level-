using Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMessaging(assemblies);
        return services;
    }
}
