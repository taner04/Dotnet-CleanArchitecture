using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ExceptionHandler;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Unexpected error",
                Detail = "An unexpected error occurred while processing your request.",
                Status = StatusCodes.Status500InternalServerError,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            },
            HttpContext = httpContext,
            Exception = exception
        });
    }
}