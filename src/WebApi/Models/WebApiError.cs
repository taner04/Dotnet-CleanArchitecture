using Domain.Common;
using ErrorOr;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace WebApi.Models;

#nullable disable

/// <summary>
///     Represents a custom error response with problem details for API error handling.
/// </summary>
public class WebApiError
{
    [JsonConstructor]
    public WebApiError()
    {
    } // For JSON deserialization

    /// <summary>
    ///     Initializes a new instance of <see cref="WebApiError" /> for validation errors.
    /// </summary>
    /// <param name="validationErrors">A dictionary of validation errors.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    public WebApiError(Dictionary<string, List<string>> validationErrors, HttpContext httpContext) : this(httpContext)
    {
        Title = "Validation.Error";
        Type = "One or more validation errors occurred.";
        Status = GetStatusCode(ErrorType.Validation);
        Errors = validationErrors;
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="WebApiError" /> for a general error.
    /// </summary>
    /// <param name="error">The error object.</param>
    /// <param name="httpContext">The current HTTP context.</param>
    public WebApiError(Error error, HttpContext httpContext) : this(httpContext)
    {
        Title = error.Code;
        Type = error.Description;
        Status = GetStatusCode(error.Type);
    }

    private WebApiError(HttpContext httpContext)
    {
        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";
        TraceId = GetTraceId(httpContext);
    }

    /// <summary>
    ///     Gets the error type describing the nature of the problem.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; private set; }

    /// <summary>
    ///     Gets the error title providing a short, human-readable summary.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; private set; }

    /// <summary>
    ///     Gets the HTTP status code associated with the error.
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; private set; }

    /// <summary>
    ///     Gets the instance string indicating the request path and method.
    /// </summary>
    [JsonProperty("instance")]
    public string Instance { get; private set; }

    /// <summary>
    ///     Gets the dictionary containing validation errors, if any.
    /// </summary>
    [JsonProperty("errors")]
    public Dictionary<string, List<string>> Errors { get; private set; } = [];

    /// <summary>
    ///     Gets the trace identifier for the request.
    /// </summary>
    [JsonProperty("traceId")]
    public string TraceId { get; private set; }

    /// <summary>
    ///     Maps an <see cref="ErrorType" /> to its corresponding HTTP status code.
    /// </summary>
    /// <param name="type">The error type.</param>
    /// <returns>The HTTP status code.</returns>
    private static int GetStatusCode(ErrorType type)
    {
        return type switch
        {
            ErrorType.NotFound => 404,
            ErrorType.Validation => 400,
            ErrorType.Conflict => 409,
            _ => 500
        };
    }

    /// <summary>
    ///     Retrieves the trace identifier from the HTTP context.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <returns>The trace identifier string.</returns>
    private static string GetTraceId(HttpContext httpContext)
    {
        return httpContext.Features.Get<IHttpActivityFeature>()?.Activity.Id!;
    }

    /// <summary>
    ///     Attempts to write a <see cref="WebApiError" /> response to the HTTP context for a given
    ///     <see cref="DomainException" />.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="domainException">The domain exception to serialize.</param>
    /// <param name="cancellationToken">A cancellation token for the async operation.</param>
    /// <returns>
    ///     A <see cref="ValueTask{Boolean}" /> indicating whether the response was successfully written.
    ///     Returns <c>true</c> if the response was written; otherwise, <c>false</c>.
    /// </returns>
    public static async ValueTask<bool> TryWriteAsync(HttpContext httpContext, DomainException domainException,
        CancellationToken cancellationToken = default)
    {
        var customError = new WebApiError(domainException.Error, httpContext);

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