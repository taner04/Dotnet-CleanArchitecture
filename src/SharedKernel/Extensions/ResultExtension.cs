using SharedKernel.Response.Errors;
using SharedKernel.Response.Results;

namespace SharedKernel.Extensions;

/// <summary>
/// Provides extension methods for matching on <see cref="Result"/> and <see cref="ResultT{TValue}"/>.
/// </summary>
public static class ResultExtension
{
    /// <summary>
    /// Executes the appropriate function based on the success or failure of the <see cref="Result"/>.
    /// </summary>
    /// <typeparam name="T">The return type of the match functions.</typeparam>
    /// <param name="result">The result to match on.</param>
    /// <param name="onSuccess">Function to execute if the result is successful.</param>
    /// <param name="onFailure">Function to execute if the result is a failure, receiving the error.</param>
    /// <returns>The result of the executed function.</returns>
    public static T Match<T>(
        this Result result,
        Func<T> onSuccess,
        Func<Error?, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error!);
    }

    /// <summary>
    /// Executes the appropriate function based on the success or failure of the <see cref="ResultT{TValue}"/>.
    /// </summary>
    /// <typeparam name="T">The return type of the match functions.</typeparam>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to match on.</param>
    /// <param name="onSuccess">Function to execute if the result is successful, receiving the value.</param>
    /// <param name="onFailure">Function to execute if the result is a failure, receiving the error.</param>
    /// <returns>The result of the executed function.</returns>
    public static T Match<T, TValue>(
        this ResultT<TValue> result,
        Func<TValue, T> onSuccess,
        Func<Error?, T> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error!);
    }
}