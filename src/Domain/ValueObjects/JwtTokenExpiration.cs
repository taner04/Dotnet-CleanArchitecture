using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects;

/// <summary>
/// Value object representing the expiration date of a JWT token.
/// Ensures the expiration date is not in the past.
/// </summary>
[ValueObject<DateTime>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public partial class JwtTokenExpiration
{
    /// <summary>
    /// Validates that the provided expiration date is not in the past.
    /// </summary>
    /// <param name="input">The expiration date to validate.</param>
    /// <returns>
    /// <see cref="Validation.Invalid"/> if the date is in the past; otherwise, <see cref="Validation.Ok"/>.
    /// </returns>
    private static Validation Validate(DateTime input)
    {
        return input < DateTime.UtcNow
            ? Validation.Invalid("The expiration date cannot be in the past.")
            : Validation.Ok;
    }
}