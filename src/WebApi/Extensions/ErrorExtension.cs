using ErrorOr;
using WebApi.Models;

namespace WebApi.Extensions;

public static class ErrorExtension
{
    public static BadRequestObjectResult ToActionResult(this List<Error> errors, HttpContext httpContext)
    {
        return errors.All(e => e.Type == ErrorType.Validation)
            ? ValidationError(errors, httpContext)
            : new BadRequestObjectResult(errors[0]);
    }

    private static BadRequestObjectResult ValidationError(List<Error> errors, HttpContext httpContext)
    {
        var validationErrors = new Dictionary<string, List<string>>();
        foreach (var error in errors)
        {
            if (validationErrors.TryGetValue(error.Code, out var value))
            {
                var errorList = value.ToList();
                errorList.Add(error.Description);
                validationErrors[error.Code] = errorList;
            }
            else
            {
                validationErrors[error.Code] = [error.Description];
            }
        }

        return new BadRequestObjectResult(new CustomError(validationErrors, httpContext));
    }
}