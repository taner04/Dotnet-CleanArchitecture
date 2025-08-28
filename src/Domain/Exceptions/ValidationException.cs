namespace Domain.Exceptions;

public class ValidationException(Error error)
    : Exception("One or more validation errors occurred.")
{
    public Error Error { get; } = error;
}