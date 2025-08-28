using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ExceptionHandler;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            ProblemDetails = CreateProblemDetails(exception, httpContext),
            HttpContext = httpContext,
            Exception = exception
        });
    }

    private static ProblemDetails CreateProblemDetails(Exception exception, HttpContext httpContext)
    {
        switch (exception)
        {
            case ValidationException validationException:
                var error = validationException.Error;
                httpContext.Response.StatusCode = error.StatusCode;
                return new ProblemDetails
                {
                    Title = error.Title,
                    Detail = error.Message,
                    Status = error.StatusCode,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    Extensions =
                    {
                        ["validationErrors"] = error.ValidationErrors
                    }
                };
            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "Unexpected error",
                    Detail = "An unexpected error occurred while processing your request.",
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                };
        }
    }
}