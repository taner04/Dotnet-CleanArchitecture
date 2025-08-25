namespace Domain.Identifiers;

[ValueObject<Guid>
(fromPrimitiveCasting: CastOperator.Implicit,
    toPrimitiveCasting: CastOperator.Implicit)]
public readonly partial struct JwtId
{
    public static Validation Validate(Guid jwtId)
    {
        return jwtId == Guid.Empty ? Validation.Invalid("The JwtId cannot be an empty GUID.") : Validation.Ok;
    }
}