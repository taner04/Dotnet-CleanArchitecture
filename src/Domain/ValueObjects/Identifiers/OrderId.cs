namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct OrderId
{
    public static Validation Validate(OrderId orderId)
    {
        return orderId == Guid.Empty ? Validation.Invalid("The OrderId cannot be an empty GUID.") : Validation.Ok;
    }
}