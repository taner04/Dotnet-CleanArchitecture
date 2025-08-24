namespace Domain.Exceptions;

public class ValueBelowMinimumException(string message) : Exception(message);
