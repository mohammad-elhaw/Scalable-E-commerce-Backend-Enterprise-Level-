using Application.Abstractions.EventBus;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus;

public static class EventBusConfiguration
{
    public static IServiceCollection AddCap<TDbContext>(
        this IServiceCollection services,
        IConfiguration config)
        where TDbContext : DbContext
    {
            services.AddCap(opts =>
            {
                opts.UseEntityFramework<TDbContext>();
                ConfigureRabbitMq(opts, config);
                opts.FailedRetryCount = 5;
                opts.FailedRetryInterval = 30;
                opts.DefaultGroupName = config["MesssageBroker:Group"] ?? "ecommerce";
            });
    
            services.AddScoped<IEventBus, CapEventBus>();

        return services;
    }

    private static void ConfigureRabbitMq(CapOptions options, IConfiguration config)
    {
        options.UseRabbitMQ(cfg =>
        {
            cfg.HostName = config["MessageBroker:Host"]!;
            cfg.Port = int.Parse(config["MessageBroker:Port"] ?? "5672");
            cfg.UserName = config["MessageBroker:Username"]!;
            cfg.Password = config["MessageBroker:Password"]!;
            cfg.VirtualHost = config["MessageBroker:VirtualHost"] ?? "/";
        });
    }
}
