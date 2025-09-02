namespace Domain.ValueObjects;

[ValueObject<string>]
public readonly partial struct Password
{
    public static Validation Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Validation.Invalid("Password cannot be null or empty.");
        }

        if (value.Length < 8)
        {
            return Validation.Invalid("Password must be at least 8 characters long.");
        }

        if (!value.Any(char.IsUpper))
        {
            return Validation.Invalid("Password must contain at least one uppercase letter.");
        }

        if (!value.Any(char.IsLower))
        {
            return Validation.Invalid("Password must contain at least one lowercase letter.");
        }

        if (!value.Any(char.IsDigit))
        {
            return Validation.Invalid("Password must contain at least one digit.");
        }

        if (value.All(char.IsLetterOrDigit))
        {
            return Validation.Invalid("Password must contain at least one special character.");
        }

        return Validation.Ok;
    }
}