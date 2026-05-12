using Application.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Messaging;

internal static class MessagingConfiguration
{
    internal static IServiceCollection AddMessaging(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
        });

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
