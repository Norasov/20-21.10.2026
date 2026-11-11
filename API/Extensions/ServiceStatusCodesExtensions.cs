namespace API.Extensions;

using Application.HttpStatusCodes;
using Microsoft.AspNetCore.Mvc;
using StatusCodes = Application.HttpStatusCodes.StatusCodes;

public static class ServiceStatusCodesExtension
{
    public static IActionResult ToActionResult<T>(this ControllerBase controller, ServiceStatusCodes<T> result)
    {
        return result.Type switch
        {
            StatusCodes.Ok => controller.Ok(result.Data),
            StatusCodes.Created => controller.StatusCode(201, result.Data),
            StatusCodes.NoContent => controller.NoContent(),
            StatusCodes.BadRequest => controller.BadRequest(result.Error),
            StatusCodes.NotFound => controller.NotFound(result.Error),
            StatusCodes.Conflict => controller.Conflict(result.Error),
            _=> controller.StatusCode(500)
        };
    }
}