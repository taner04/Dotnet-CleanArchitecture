using Domain.Common.Interfaces;
using Vogen;

namespace Domain.ValueObjects;

/// <summary>
/// Represents a value object for monetary values.
/// </summary>
[ValueObject<decimal>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public partial class Money
{
    /// <summary>
    /// Validates that the monetary value is not less than zero.
    /// </summary>
    /// <param name="input">The decimal value to validate.</param>
    /// <returns>
    /// Returns <see cref="Validation.Invalid"/> if the value is less than zero,
    /// otherwise returns <see cref="Validation.Ok"/>.
    /// </returns>
    private static Validation Validate(decimal input)
    {
        return input < 0 ? Validation.Invalid("The value cannot be less then 0.") : Validation.Ok;
    }
}