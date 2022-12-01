using ErrorOr;

using Microsoft.AspNetCore.Mvc;

using Workers.Api.Common.Constant;

namespace Workers.Api.Controllers;

[ApiController]
public abstract class ApiController<T> : ControllerBase  where T : ApiController<T>
{
    private ILogger<T>? _logger;

    protected ILogger<T>? Logger => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextKeys.ProblemErrors] = errors;

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,

        };

        Logger?.LogError($"Error: {statusCode}, Message: {firstError.Description}");

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}