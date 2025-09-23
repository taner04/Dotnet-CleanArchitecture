using ErrorOr;

namespace SharedKernel.Errors;

public static class UserErrors
{
    public static Error Unauthorized => Error.Unexpected(
        "User.Unauthorized",
        "User is unauthorized.");

    public static Error InvalidCredentials => Error.Unauthorized(
        "User.InvalidCredentials",
        "The provided credentials are invalid.");

    public static Error InvalidEmail => Error.Validation(
        "User.InvalidEmail",
        "The provided email is invalid.");

    public static Error AlreadyExists => Error.Conflict(
        "User.AlreadyExists",
        "A user with the given details already exists.");

    public static Error InvalidName => Error.Validation(
        "User.InvalidName",
        "The provided name is invalid.");
}