namespace SharedKernel.Response
{
    public static class ErrorFactory
    {
        public static Error NotFound(string message) =>
            new(ErrorTitles.NotFound, message, 404);

        public static Error BadRequest(string message) =>
            new(ErrorTitles.BadRequest, message, 400);

        public static Error Unauthorized(string message) =>
            new(ErrorTitles.Unauthorized, message, 401);

        public static Error Forbidden(string message) =>
            new(ErrorTitles.Forbidden, message, 403); 

        public static Error InternalServerError(string message) =>
            new(ErrorTitles.InternalServerError, message, 500);

        public static Error ValidationError(IDictionary<string, string[]> validationErrors) =>
            new(ErrorTitles.ValidationError, "Some properties are missing", 422, validationErrors);

        public static Error Conflict(string message) =>
            new("Conflict", message, 409);

        public static class ErrorTitles
        {
            public const string NotFound = "Not Found";
            public const string BadRequest = "Bad Request";
            public const string Unauthorized = "Unauthorized";
            public const string Forbidden = "Forbidden";
            public const string InternalServerError = "Internal Server Error";
            public const string ValidationError = "Validation Error";
        }
    }

}
