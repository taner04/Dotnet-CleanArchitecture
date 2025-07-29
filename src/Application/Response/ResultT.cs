namespace Application.Response
{
    public class ResultT<TValue> 
    {
        private readonly Error? _error;
        private readonly TValue? _value;

        private ResultT(TValue value)
        {
            _value = value;
        }

        private ResultT(Error? error)
        {
            _error = error;
        }

        public static ResultT<TValue> Success(TValue value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null for a successful result.");
            }
            return new ResultT<TValue>(value);
        }

        public static ResultT<TValue> Failure(Error error)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error), "Error cannot be null for a failure result.");
            }
            return new ResultT<TValue>(error);
        }

        public static ResultT<TValue> Failure(
            string title,
            string message,
            int statusCode,
            Dictionary<string, string[]>? errors = default!) => Failure(new(title, message, statusCode, errors));


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
