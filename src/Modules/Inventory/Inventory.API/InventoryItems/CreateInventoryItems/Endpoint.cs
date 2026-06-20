using Inventory.Application.InventoryItems.CreateInventoryItem;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.InventoryItems.CreateInventoryItems;

public sealed class Endpoint(IMediator mediator)
    : BaseController
{
    [HttpPost]
    public async Task<ActionResult<Response>> CreateInventoryItem(Request request)
    {
        var command = new CreateInventoryItemCommand(request.ProductVariantId, request.WarehouseId);
        var result = await mediator.Send(command);
        return HandleResult(result, StatusCodes.Status201Created, id => new Response(id));
    }
}