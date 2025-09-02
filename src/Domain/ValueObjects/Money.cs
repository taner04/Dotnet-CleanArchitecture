using Vogen;

namespace Domain.ValueObjects;

[ValueObject<decimal>]
public readonly partial struct Money
{
    public static Validation Validate(decimal amount)
    {
        return amount < 0 ? Validation.Invalid("The amount cannot be negative.") : Validation.Ok;
    }
}