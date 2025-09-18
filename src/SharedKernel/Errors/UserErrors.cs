using ErrorOr;

namespace SharedKernel.Errors;

public static class UserErrors
{
    public static Error Unauthorized => Error.Unexpected(
        code: "User.Unauthorized",
        description: "User is unauthorized.");
    
    public static Error InvalidCredentials => Error.Unauthorized(
        code: "User.InvalidCredentials",
        description: "The provided credentials are invalid.");
    
    public static Error InvalidEmail => Error.Validation(
        code: "User.InvalidEmail",
        description: "The provided email is invalid.");
    
    public static Error AlreadyExists => Error.Conflict(
        code: "User.AlreadyExists",
        description: "A user with the given details already exists.");
    
    public static Error InvalidName => Error.Validation(
        code: "User.InvalidName",
        description: "The provided name is invalid.");
}