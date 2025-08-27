namespace Domain.Identifiers;

/// <summary>
/// Represents a strongly-typed identifier for a cart.
/// </summary>
[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct CartId
{
    /// <summary>
    /// Validates the specified <see cref="CartId"/> to ensure it is not an empty GUID.
    /// </summary>
    /// <param name="cartId">The cart identifier to validate.</param>
    /// <returns>
    /// <see cref="Validation.Invalid"/> if the identifier is empty; otherwise, <see cref="Validation.Ok"/>.
    /// </returns>
    public static Validation Validate(CartId cartId)
    {
        return cartId == Guid.Empty ? Validation.Invalid("The CartId cannot be an empty GUID.") : Validation.Ok;
    }
}