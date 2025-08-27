namespace SharedKernel.Response.Errors;

/// <summary>
/// Represents an error with a title, message, status code, and optional validation errors.
/// </summary>
/// <param name="Title">A short, descriptive title for the error.</param>
/// <param name="Message">A detailed message describing the error.</param>
/// <param name="StatusCode">The HTTP status code associated with the error.</param>
/// <param name="ValidationErrors">
/// An optional dictionary containing validation errors, where the key is the field name 
/// and the value is an array of error messages for that field.
/// </param>
public readonly record struct Error(
    string Title,
    string Message,
    int StatusCode,
    IDictionary<string, string[]>? ValidationErrors = null
);