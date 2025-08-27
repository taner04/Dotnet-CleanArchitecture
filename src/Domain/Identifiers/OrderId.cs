namespace Domain.Identifiers;

/// <summary>
/// Represents a strongly-typed identifier for an Order.
/// </summary>
[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct OrderId
{
    /// <summary>
    /// Validates the specified <see cref="OrderId"/> to ensure it is not an empty GUID.
    /// </summary>
    /// <param name="orderId">The <see cref="OrderId"/> to validate.</param>
    /// <returns>
    /// <see cref="Validation.Invalid"/> if the identifier is empty; otherwise, <see cref="Validation.Ok"/>.
    /// </returns>
    public static Validation Validate(OrderId orderId)
    {
        return orderId == Guid.Empty ? Validation.Invalid("The OrderId cannot be an empty GUID.") : Validation.Ok;
    }
}