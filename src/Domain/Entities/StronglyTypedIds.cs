using Vogen;

namespace Domain.Entities
{
    [ValueObject<Guid>
        (fromPrimitiveCasting: CastOperator.Implicit,
         toPrimitiveCasting: CastOperator.Implicit)]
    public readonly partial struct JwtId;

    [ValueObject<Guid>
        (fromPrimitiveCasting: CastOperator.Implicit,
         toPrimitiveCasting: CastOperator.Implicit)]
    public readonly partial struct OrderId;

    [ValueObject<Guid>
        (fromPrimitiveCasting: CastOperator.Implicit,
         toPrimitiveCasting: CastOperator.Implicit)]
    public readonly partial struct OrderItemId;

    [ValueObject<Guid>
        (fromPrimitiveCasting: CastOperator.Implicit,
         toPrimitiveCasting: CastOperator.Implicit)]
    public readonly partial struct ProductId;

    [ValueObject<Guid>
        (fromPrimitiveCasting: CastOperator.Implicit,
         toPrimitiveCasting: CastOperator.Implicit)]
    public readonly partial struct UserId;
}
