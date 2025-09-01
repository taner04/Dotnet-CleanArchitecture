namespace Domain.ValueObjects.Identifiers;

[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct OrderItemId
{
    public static Validation Validate(Guid orderItemId)
    {
        return orderItemId == Guid.Empty
            ? Validation.Invalid("The OrderItemId cannot be an empty GUID.")
            : Validation.Ok;
    }
}