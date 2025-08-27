namespace Domain.Identifiers;

/// <summary>
/// Represents a strongly-typed identifier for a user.
/// </summary>
/// <remarks>
/// Uses implicit casting to and from <see cref="Guid"/>.
/// </remarks>
[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct UserId
{
    /// <summary>
    /// Validates the specified <see cref="Guid"/> as a <see cref="UserId"/>.
    /// </summary>
    /// <param name="userId">The GUID to validate.</param>
    /// <returns>
    /// <see cref="Validation.Invalid"/> if the GUID is empty; otherwise, <see cref="Validation.Ok"/>.
    /// </returns>
    public static Validation Validate(Guid userId)
    {
        return userId == Guid.Empty ? Validation.Invalid("The UserId cannot be an empty GUID.") : Validation.Ok;
    }
}