using Vogen;

namespace Domain.ValueObjects;

[ValueObject<DateTime>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public partial class JwtTokenExpiration
{
    private static Validation Validate(DateTime input)
    {
        return input < DateTime.UtcNow
            ? Validation.Invalid("The expiration date cannot be in the past.")
            : Validation.Ok;
    }
}