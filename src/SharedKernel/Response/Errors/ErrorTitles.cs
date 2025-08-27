namespace SharedKernel.Response.Errors;

/// <summary>
/// Provides constant error titles for common HTTP error responses.
/// </summary>
public static class ErrorTitles
{
    /// <summary>
    /// Error title for 404 Not Found.
    /// </summary>
    public const string NotFound = "Not Found";

    /// <summary>
    /// Error title for 400 Bad Request.
    /// </summary>
    public const string BadRequest = "Bad Request";

    /// <summary>
    /// Error title for 401 Unauthorized.
    /// </summary>
    public const string Unauthorized = "Unauthorized";

    /// <summary>
    /// Error title for 403 Forbidden.
    /// </summary>
    public const string Forbidden = "Forbidden";

    /// <summary>
    /// Error title for 500 Internal Server Error.
    /// </summary>
    public const string InternalServerError = "Internal Server Error";

    /// <summary>
    /// Error title for validation errors.
    /// </summary>
    public const string ValidationError = "Validation Error";
}