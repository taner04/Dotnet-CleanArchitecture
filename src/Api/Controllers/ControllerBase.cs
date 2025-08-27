using Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Extensions;
using SharedKernel.Response;

namespace Api.Controllers;

[ApiController]
public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected IActionResult MapResponse<T>(ResultT<T> result)
    {
        return result.Match(
            value => Ok(value),
            CreateError
        );
    }

    protected IActionResult MapResponse(Result result)
    {
        return result.Match<IActionResult>(
            Ok,
            CreateError
        );
    }

    private ObjectResult CreateError(Error? error)
    {
        var problemDetails = new ProblemDetails
        {
            Title = error?.Title,
            Detail = error?.Message,
            Status = error?.StatusCode,
            Instance = $"{HttpContext.Request.Method} {HttpContext.Request.Path}",
            Extensions = { ["traceId"] = HttpContext.TraceIdentifier }
        };

        if (error?.ValidationErrors is { Count: > 0 })
            problemDetails.Extensions["validationErrors"] = error?.ValidationErrors;

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
            ContentTypes = { "application/problem+json" }
        };
    }
}