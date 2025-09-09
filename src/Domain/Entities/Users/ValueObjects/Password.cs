using Vogen;

namespace Domain.Entities.Users.ValueObjects;

[ValueObject<string>]
public readonly partial struct Password
{
    public static Validation Validate(string password)
    {
        return password.Length >= 8
            ? Validation.Ok
            : Validation.Invalid("Password must be at least 8 characters long.");
    }
}