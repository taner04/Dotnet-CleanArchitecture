using Domain.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.ExceptionHandler;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var type = exception.GetType().Name;
        var instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";
        
        var statusCode = exception switch
        {
            DomainException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var title = statusCode switch
        {
            StatusCodes.Status400BadRequest => "Domain error",
            _ => "Unexpected error"
        };
        
        var detail = exception switch
        {
            DomainException domainException => domainException.Message,
            _ => "An unexpected error occurred. Please try again later."
        };
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            ProblemDetails = new ProblemDetails
            {
                Type = type,
                Title = title,
                Detail = detail,
                Status = statusCode,
                Instance = instance
            },
            HttpContext = httpContext,
            Exception = exception
        });
    }
}