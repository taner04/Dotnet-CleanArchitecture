namespace Domain.Identifiers;

/// <summary>
/// Represents a strongly-typed identifier for a cart item.
/// </summary>
[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct CartItemId
{
    /// <summary>
    /// Validates the specified <see cref="CartItemId"/>.
    /// Returns invalid if the identifier is an empty GUID.
    /// </summary>
    /// <param name="cartItemId">The cart item identifier to validate.</param>
    /// <returns>A <see cref="Validation"/> result indicating validity.</returns>
    public static Validation Validate(CartItemId cartItemId)
    {
        return cartItemId == Guid.Empty ? Validation.Invalid("The CartItemId cannot be an empty GUID.") : Validation.Ok;
    }
}