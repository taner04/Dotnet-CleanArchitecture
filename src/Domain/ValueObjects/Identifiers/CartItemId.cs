namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct CartItemId
{
    public static Validation Validate(CartItemId cartItemId)
    {
        return cartItemId == Guid.Empty ? Validation.Invalid("The CartItemId cannot be an empty GUID.") : Validation.Ok;
    }
}