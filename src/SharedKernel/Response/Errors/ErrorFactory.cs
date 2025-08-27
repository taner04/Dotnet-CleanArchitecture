namespace SharedKernel.Response.Errors;

/// <summary>
/// Factory class for creating standardized <see cref="Error"/> instances for common HTTP error scenarios.
/// </summary>
public static class ErrorFactory
{
    /// <summary>
    /// Creates a NotFound error (HTTP 404).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>An <see cref="Error"/> representing a NotFound error.</returns>
    public static Error NotFound(string message)
    {
        return new Error(ErrorTitles.NotFound, message, 404);
    }

    /// <summary>
    /// Creates a BadRequest error (HTTP 400).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>An <see cref="Error"/> representing a BadRequest error.</returns>
    public static Error BadRequest(string message)
    {
        return new Error(ErrorTitles.BadRequest, message, 400);
    }

    /// <summary>
    /// Creates an Unauthorized error (HTTP 401).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>An <see cref="Error"/> representing an Unauthorized error.</returns>
    public static Error Unauthorized(string message)
    {
        return new Error(ErrorTitles.Unauthorized, message, 401);
    }

    /// <summary>
    /// Creates a Forbidden error (HTTP 403).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>An <see cref="Error"/> representing a Forbidden error.</returns>
    public static Error Forbidden(string message)
    {
        return new Error(ErrorTitles.Forbidden, message, 403);
    }

    /// <summary>
    /// Creates an InternalServerError error (HTTP 500).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>An <see cref="Error"/> representing an InternalServerError error.</returns>
    public static Error InternalServerError(string message)
    {
        return new Error(ErrorTitles.InternalServerError, message, 500);
    }

    /// <summary>
    /// Creates a ValidationError error (HTTP 422) with validation details.
    /// </summary>
    /// <param name="validationErrors">Dictionary of validation errors.</param>
    /// <returns>An <see cref="Error"/> representing a ValidationError error.</returns>
    public static Error ValidationError(IDictionary<string, string[]> validationErrors)
    {
        return new Error(ErrorTitles.ValidationError, "Some properties are missing", 422, validationErrors);
    }

    /// <summary>
    /// Creates a Conflict error (HTTP 409).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <returns>An <see cref="Error"/> representing a Conflict error.</returns>
    public static Error Conflict(string message)
    {
        return new Error("Conflict", message, 409);
    }
}