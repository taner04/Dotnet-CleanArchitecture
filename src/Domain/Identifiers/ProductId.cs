namespace Domain.Identifiers;

/// <summary>
/// Represents a strongly-typed identifier for a Product.
/// </summary>
[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct ProductId
{
    /// <summary>
    /// Validates that the provided <paramref name="orderId"/> is not an empty GUID.
    /// </summary>
    /// <param name="orderId">The GUID to validate as a ProductId.</param>
    /// <returns>
    /// <see cref="Validation.Invalid"/> if <paramref name="orderId"/> is empty; otherwise <see cref="Validation.Ok"/>.
    /// </returns>
    public static Validation Validate(Guid orderId)
    {
        return orderId == Guid.Empty ? Validation.Invalid("The ProductId cannot be an empty GUID.") : Validation.Ok;
    }
}