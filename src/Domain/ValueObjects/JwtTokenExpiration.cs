using Vogen;

namespace Domain.ValueObjects;

[ValueObject<DateTime>]
public readonly partial struct JwtTokenExpiration
{
    public static Validation Validate(DateTime expiration)
    {
        return expiration <= DateTime.UtcNow
            ? Validation.Invalid("The JWT token expiration must be a future date and time.")
            : Validation.Ok;
    }
}