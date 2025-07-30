using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected IActionResult MapResponse<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            var problemDetails = new ProblemDetails
            {
                Title = result.Error?.Title,
                Detail = result.Error?.Message,
                Status = result.Error?.StatusCode,
                Instance = $"{HttpContext.Request.Method} {HttpContext.Request.Path}"
            };

            problemDetails.Extensions["traceId"] = HttpContext.TraceIdentifier;

            if (result.Error?.ValidationErrors is { Count: > 0 })
            {
                problemDetails.Extensions["validationErrors"] = result.Error.Value.ValidationErrors;
            }

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status,
                ContentTypes = { "application/problem+json" }
            };
        }
    }
}
