using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects;

/// <summary>
/// Represents a JWT token value object with validation.
/// </summary>
[ValueObject<string>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public partial class JwtToken
{
    /// <summary>
    /// Validates the JWT token string.
    /// Returns invalid if the input is null or empty.
    /// </summary>
    /// <param name="input">The JWT token string to validate.</param>
    /// <returns>A <see cref="Validation"/> result indicating validity.</returns>
    private static Validation Validate(string input)
    {
        return string.IsNullOrWhiteSpace(input)
            ? Validation.Invalid("The JWT token cannot be null or empty.")
            : Validation.Ok;
    }
}