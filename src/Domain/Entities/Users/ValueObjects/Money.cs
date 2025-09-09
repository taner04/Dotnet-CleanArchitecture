using Vogen;

namespace Domain.Entities.Users.ValueObjects;

[ValueObject<decimal>]
public readonly partial struct  Money
{
    public static Validation Validate(decimal value)
    {
        return value >= 0
            ? Validation.Ok
            : Validation.Invalid("Money value cannot be negative.");
    }
}