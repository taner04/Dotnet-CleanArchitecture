using Domain.Entities.Base;
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
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return exception switch
        {
            DomainException domainException => new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Domain error occured",
                Detail = domainException.Message,
                Status = StatusCodes.Status500InternalServerError,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            },
            _ => new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Unexpected error",
                Detail = "An unexpected error occurred while processing your request.",
                Status = StatusCodes.Status500InternalServerError,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            }
        };
    }
}