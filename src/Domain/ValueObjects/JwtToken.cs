using Vogen;

namespace Domain.ValueObjects;

[ValueObject<string>]
public readonly partial struct JwtToken
{
    public static Validation Validate(string token)
    {
        return string.IsNullOrWhiteSpace(token) ? Validation.Invalid("The JWT token cannot be null or empty.") : Validation.Ok;
    }
}