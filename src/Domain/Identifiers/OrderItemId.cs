namespace Domain.Identifiers;

/// <summary>
/// Represents a strongly-typed identifier for an Order Item.
/// </summary>
[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct OrderItemId
{
    /// <summary>
    /// Validates the provided <paramref name="orderItemId"/> to ensure it is not an empty GUID.
    /// </summary>
    /// <param name="orderItemId">The GUID to validate as an OrderItemId.</param>
    /// <returns>
    /// <see cref="Validation.Invalid"/> if the GUID is empty; otherwise, <see cref="Validation.Ok"/>.
    /// </returns>
    public static Validation Validate(Guid orderItemId)
    {
        return orderItemId == Guid.Empty
            ? Validation.Invalid("The OrderItemId cannot be an empty GUID.")
            : Validation.Ok;
    }
}