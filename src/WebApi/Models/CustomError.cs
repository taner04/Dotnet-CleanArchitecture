using Domain.Common;
using ErrorOr;
using Microsoft.AspNetCore.Http.Features;

namespace WebApi.Models;

/// <summary>
/// Represents a custom error response with problem details for API error handling.
/// </summary>
public class CustomError
{
    /// <summary>
    /// Gets the problem details describing the error.
    /// </summary>
    public ProblemDetails ProblemDetails { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="CustomError"/> for validation errors.
    /// </summary>
    /// <param name="validationErrors">A dictionary of validation errors.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    public CustomError(Dictionary<string, List<string>> validationErrors, HttpContext httpContext)
    {
        ProblemDetails = CreateProblemDetails(
            "One or more validation errors occurred.",
            "ValidationError",
            GetStatusCode(ErrorType.Validation),
            httpContext,
            new Dictionary<string, object>
            {
                ["errors"] = validationErrors
            }
        );
    }

    /// <summary>
    /// Initializes a new instance of <see cref="CustomError"/> for a general error.
    /// </summary>
    /// <param name="error">The error object.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    public CustomError(Error error, HttpContext httpContext)
    {
        ProblemDetails = CreateProblemDetails(
            error.Description,
            error.Code,
            GetStatusCode(error.Type),
            httpContext,
            new Dictionary<string, object>()
        );
    }

    /// <summary>
    /// Creates a <see cref="ProblemDetails"/> object with the specified parameters.
    /// </summary>
    /// <param name="title">The title of the problem.</param>
    /// <param name="type">The type of the problem.</param>
    /// <param name="status">The HTTP status code.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="extensions">Additional extension data.</param>
    /// <returns>A populated <see cref="ProblemDetails"/> object.</returns>
    private static ProblemDetails CreateProblemDetails(
        string title,
        string type,
        int status,
        HttpContext httpContext,
        Dictionary<string, object> extensions)
    {
        extensions.Add("traceId", GetTraceId(httpContext));
        
        return new ProblemDetails
        {
            Title = title,
            Type = type,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Status = status,
            Extensions = extensions
        };
    }

    /// <summary>
    /// Maps an <see cref="ErrorType"/> to its corresponding HTTP status code.
    /// </summary>
    /// <param name="type">The error type.</param>
    /// <returns>The HTTP status code.</returns>
    private static int GetStatusCode(ErrorType type) => type switch
    {
        ErrorType.NotFound => 404,
        ErrorType.Validation => 400,
        ErrorType.Conflict => 409,
        _ => 500
    };

    /// <summary>
    /// Retrieves the trace identifier from the HTTP context.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <returns>The trace identifier string.</returns>
    private static string GetTraceId(HttpContext httpContext) =>
        httpContext.Features.Get<IHttpActivityFeature>()?.Activity.Id!;

    /// <summary>
    /// Attempts to write a <see cref="CustomError"/> response to the HTTP context for a given <see cref="DomainException"/>.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="domainException">The domain exception to serialize.</param>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{Boolean}"/> indicating whether the response was successfully written.
    /// Returns <c>true</c> if the response was written; otherwise, <c>false</c>.
    /// </returns>
    public static async ValueTask<bool> TryWriteAsync(HttpContext httpContext, DomainException domainException, CancellationToken cancellationToken = default)
    {
        var customError = new CustomError(domainException.Error, httpContext);

        httpContext.Response.StatusCode = GetStatusCode(domainException.Error.Type);
        httpContext.Response.ContentType = "application/json";

        try
        {
            await httpContext.Response.WriteAsJsonAsync(customError, cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}