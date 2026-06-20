using Infrastructure;
using Inventory.API;
using Inventory.Infrastructure;
using Inventory.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Gateway.Extensions;

public static class InitializeApplicationServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(Inventory.API.AssemblyReference).Assembly)
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value!.Errors.Count > 0)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var response = new ApiErrorResponse
                        (
                            Code: "ValidationError",
                            Message: "One Or More Validation Errors Occured",
                            Errors: errors
                        );

                    return new BadRequestObjectResult(response);
                };
            });

        services.AddInventoryModule(config);

        services.AddInfrastructure(
            typeof(Inventory.Application.AssemblyReference).Assembly);

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
