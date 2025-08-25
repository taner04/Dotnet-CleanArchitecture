namespace Domain.Identifiers;

[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct UserId
{
    public static Validation Validate(Guid userId)
    {
        return userId == Guid.Empty ? Validation.Invalid("The UserId cannot be an empty GUID.") : Validation.Ok;
    }
}