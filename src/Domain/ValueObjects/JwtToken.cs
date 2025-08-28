using Vogen;

namespace Domain.ValueObjects;

[ValueObject<string>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public partial class JwtToken
{
    private static Validation Validate(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            ? Validation.Invalid("The JWT token cannot be null or empty.")
            : Validation.Ok;
    }
}