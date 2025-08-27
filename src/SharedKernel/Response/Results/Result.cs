using SharedKernel.Response.Errors;

namespace SharedKernel.Response.Results;

/// <summary>
/// Represents the result of an operation, indicating success or failure.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a successful result.
    /// </summary>
    protected Result()
    {
        IsSuccess = true;
        Error = null;
    }
    
    /// <summary>
    /// Initializes a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error associated with the failure.</param>
    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }
    
    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    public bool IsSuccess { get; }
    
    /// <summary>
    /// Gets the error associated with the result, if any.
    /// </summary>
    public Error? Error { get; }
    
    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a failed <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result(Error error)
    {
        return new Result(error);
    }
    
    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new();
    
    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error associated with the failure.</param>
    public static Result Failure(Error error) => new(error);
}