using Vogen;

namespace Domain.Entities.ApplicationUsers.ValueObjects;

[ValueObject<string>]
public readonly partial struct Password
{
    public static Validation Validate(string value)
    {
        return value.Length >= 8
            ? Validation.Ok
            : Validation.Invalid("Password must be at least 8 characters long.");
    }
}