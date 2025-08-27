using SharedKernel.Response.Errors;

namespace SharedKernel.Response.Results;

/// <summary>
/// Represents the result of an operation that returns a value of type <typeparamref name="TValue"/>.
/// Inherits from <see cref="Result"/>.
/// </summary>
/// <typeparam name="TValue">The type of the value returned by the operation.</typeparam>
public sealed class ResultT<TValue> : Result
{
    private readonly TValue? _value;
    /// <summary>
    /// Initializes a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    private ResultT(TValue value) : base()
    {
        _value = value;
    }
    /// <summary>
    /// Initializes a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure.</param>
    private ResultT(Error error) : base(error)
    {
        _value = default;
    }
    /// <summary>
    /// Gets the value of the result if successful; otherwise, throws an exception.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the result is not successful.</exception>
    public TValue Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("Value can not be accessed when IsSuccess is false");
    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a failed <see cref="ResultT{TValue}"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator ResultT<TValue>(Error error)
    {
        return new ResultT<TValue>(error);
    }
    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="TValue"/> to a successful <see cref="ResultT{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator ResultT<TValue>(TValue value)
    {
        return new ResultT<TValue>(value);
    }
    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value of the successful result.</param>
    /// <returns>A successful <see cref="ResultT{TValue}"/>.</returns>
    public static ResultT<TValue> Success(TValue value) => new(value);
    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure.</param>
    /// <returns>A failed <see cref="ResultT{TValue}"/>.</returns>
    public new static ResultT<TValue> Failure(Error error) => new(error);
}