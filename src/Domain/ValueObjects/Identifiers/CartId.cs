namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct CartId
{
    public static Validation Validate(CartId cartId)
    {
        return cartId == Guid.Empty ? Validation.Invalid("The CartId cannot be an empty GUID.") : Validation.Ok;
    }
}