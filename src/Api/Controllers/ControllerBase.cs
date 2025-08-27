using Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Extensions;
using SharedKernel.Response;
using SharedKernel.Response.Errors;
using SharedKernel.Response.Results;

namespace Api.Controllers;

/// <summary>
/// Base controller providing standardized response mapping for API endpoints.
/// </summary>
[ApiController]
public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Maps a <see cref="ResultT{T}"/> to an <see cref="IActionResult"/>.
    /// Returns Ok for success or a problem response for errors.
    /// </summary>
    /// <typeparam name="T">Type of the successful result value.</typeparam>
    /// <param name="result">The result to map.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    protected IActionResult MapResponse<T>(ResultT<T> result)
    {
        return result.Match(
            value => Ok(value),
            CreateError
            );
    }
    
    /// <summary>
    /// Maps a <see cref="Result"/> to an <see cref="IActionResult"/>.
    /// Returns Ok for success or a problem response for errors.
    /// </summary>
    /// <param name="result">The result to map.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result.</returns>
    protected IActionResult MapResponse(Result result)
    {
        return result.Match<IActionResult>(
            Ok,
            CreateError
            );
    }
    
    /// <summary>
    /// Creates an <see cref="ObjectResult"/> containing <see cref="ProblemDetails"/>
    /// for the provided error, including validation errors and trace information.
    /// </summary>
    /// <param name="error">The error to represent.</param>
    /// <returns>An <see cref="ObjectResult"/> with problem details.</returns>
    private ObjectResult CreateError(Error? error)
    {
        var problemDetails = new ProblemDetails
        {
            Title = error?.Title,
                Detail = error?.Message,
                Status = error?.StatusCode,
                Instance = $"{HttpContext.Request.Method} {HttpContext.Request.Path}",
                Extensions = { ["traceId"] = HttpContext.TraceIdentifier }
        };
        
        if (error?.ValidationErrors is { Count: > 0 })
            problemDetails.Extensions["validationErrors"] = error?.ValidationErrors;
        
        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
                ContentTypes = { "application/problem+json" }
        };
    }
}