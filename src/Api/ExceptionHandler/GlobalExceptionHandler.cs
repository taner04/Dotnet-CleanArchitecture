using Api.Models;
using Domain.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.ExceptionHandler;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is DomainException domainException)
        {
            var result = await CustomError.TryWriteAsync(httpContext, domainException, cancellationToken);
            if (result)
            {
                return true;
            }
        }

        var context = new ProblemDetailsContext
        {
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Unexpected error occurred",
                Detail = "An unexpected error occurred. Please try again later.",
                Status = StatusCodes.Status500InternalServerError,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            },
            HttpContext = httpContext,
            Exception = exception
        };

        return await problemDetailsService.TryWriteAsync(context);
    }
}