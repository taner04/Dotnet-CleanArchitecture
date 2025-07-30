namespace Application.Response
{
    public class Result<TValue> 
    {
        private readonly Error? _error;
        private readonly TValue? _value;

        private Result(TValue value) => _value = value;
        private Result(Error? error) => _error = error;

        public static Result<TValue> Success(TValue value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null for a successful result.");
            }

            return new Result<TValue>(value);
        }

        public static Result<TValue> Failure(Error error) => new(error);

        public static Result<TValue> Failure(
            string title,
            string message,
            int statusCode,
            Dictionary<string, string[]>? validationErrors = default!) => Failure(new(title, message, statusCode, validationErrors));


        public bool IsSuccess => _error == null && _value != null;

        public Error? Error
        {
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("Cannot access Error when Result is successful.");
                }
                
                return _error;
            }
        }

        public TValue Value
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new InvalidOperationException("Cannot access Value when Result is not successful.");
                }

                return _value!;
            }
        }
    }
}
