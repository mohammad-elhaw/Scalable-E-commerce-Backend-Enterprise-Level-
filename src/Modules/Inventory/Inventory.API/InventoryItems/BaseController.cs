using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Inventory.API.InventoryItems;

[ApiController]
[Route("api/inventory-items")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult HandleResult(
        Result result, 
        int statusCode)
    {
        if (result.IsSuccess) return StatusCode(statusCode);

        return StatusCode(result.Error.StatusCode, new ApiErrorResponse(
            result.Error.Code,
            result.Error.Message));
    }

    protected ActionResult<TResponse> HandleResult<TValue, TResponse>(
        Result<TValue> result, 
        int statusCode,
        Func<TValue, TResponse> mapper)
    {
        if (result.IsSuccess) return StatusCode(statusCode, mapper(result.Value!));

        return StatusCode(result.Error.StatusCode, new ApiErrorResponse(
            result.Error.Code,
            result.Error.Message));
    }
}