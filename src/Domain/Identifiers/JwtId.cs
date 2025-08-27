namespace Domain.Identifiers;

/// <summary>
/// Represents a strongly-typed identifier for JWTs.
/// </summary>
/// <remarks>
/// Uses implicit cast operators to and from <see cref="Guid"/>.
/// </remarks>
[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct JwtId
{
    /// <summary>
    /// Validates that the provided <see cref="Guid"/> is not empty.
    /// </summary>
    /// <param name="jwtId">The JWT identifier to validate.</param>
    /// <returns>
    /// <see cref="Validation.Invalid"/> if <paramref name="jwtId"/> is <see cref="Guid.Empty"/>, otherwise <see cref="Validation.Ok"/>.
    /// </returns>
    public static Validation Validate(Guid jwtId)
    {
        return jwtId == Guid.Empty ? Validation.Invalid("The JwtId cannot be an empty GUID.") : Validation.Ok;
    }
}