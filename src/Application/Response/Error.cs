namespace Application.Response
{
    public class Error
    {
        private string _title;
        private string _message;
        private int _statusCode;
        private Dictionary<string, string[]>? _errors;

        public Error(string title, string message, int statusCode, Dictionary<string, string[]>? errors = default!)
        {
            _title = title;
            _message = message;
            _statusCode = statusCode;
            _errors = errors;
        }

        public string Title { get => _title; set => _title = value; }
        public string Message { get => _message; set => _message = value; }
        public int StatusCode { get => _statusCode; set => _statusCode = value; }
        public Dictionary<string, string[]>? Errors { get => _errors; set => _errors = value; }
    }
}
