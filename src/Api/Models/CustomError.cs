using ErrorOr;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    
    namespace Api.Models;
    
    public class CustomError
    {
        public ProblemDetails ProblemDetails { get; init; }
    
        public CustomError(Dictionary<string, List<string>> validationErrors, HttpContext httpContext)
        {
            ProblemDetails = CreateProblemDetails(
                "One or more validation errors occurred.",
                "ValidationError",
                GetStatusCode(ErrorType.Validation),
                httpContext,
                new Dictionary<string, object>
                {
                    ["errors"] = validationErrors,
                    ["traceId"] = GetTraceId(httpContext)
                }
            );
        }
    
        public CustomError(Error error, HttpContext httpContext)
        {
            ProblemDetails = CreateProblemDetails(
                error.Description,
                error.Code,
                GetStatusCode(error.Type),
                httpContext,
                new Dictionary<string, object>
                {
                    ["traceId"] = GetTraceId(httpContext)
                }
            );
        }
    
        private static ProblemDetails CreateProblemDetails(
            string title,
            string type,
            int status,
            HttpContext httpContext,
            Dictionary<string, object> extensions)
        {
            return new ProblemDetails
            {
                Title = title,
                Type = type,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                Status = status,
                Extensions = extensions!
            };
        }
    
        private static int GetStatusCode(ErrorType type) => type switch
        {
            ErrorType.NotFound => 404,
            ErrorType.Validation => 400,
            ErrorType.Conflict => 409,
            _ => 500
        };
    
        private static string GetTraceId(HttpContext httpContext) =>
            httpContext.Features.Get<IHttpActivityFeature>()?.Activity.Id!;
    }