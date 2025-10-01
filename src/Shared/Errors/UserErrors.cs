using ErrorOr;

namespace Shared.Errors;

public static class UserErrors
{
    public static Error Unauthorized => Error.Unexpected(
        "ApplicationUser.Unauthorized",
        "ApplicationUser is unauthorized.");

    public static Error InvalidCredentials => Error.Unauthorized(
        "ApplicationUser.InvalidCredentials",
        "The provided credentials are invalid.");

    public static Error InvalidEmail => Error.Validation(
        "ApplicationUser.InvalidEmail",
        "The provided email is invalid.");

    public static Error AlreadyExists => Error.Conflict(
        "ApplicationUser.AlreadyExists",
        "A user with the given details already exists.");

    public static Error InvalidName => Error.Validation(
        "ApplicationUser.InvalidName",
        "The provided name is invalid.");
}