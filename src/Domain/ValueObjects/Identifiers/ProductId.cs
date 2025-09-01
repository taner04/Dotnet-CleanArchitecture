namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct ProductId
{
    public static Validation Validate(Guid orderId)
    {
        return orderId == Guid.Empty ? Validation.Invalid("The ProductId cannot be an empty GUID.") : Validation.Ok;
    }
}