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
    public readonly partial struct OrderId
    {
        public static Validation Validate(OrderId orderId)
        {
            return orderId == Guid.Empty ? Validation.Invalid("The OrderId cannot be an empty GUID.") : Validation.Ok;
        }
    }

    [ValueObject<Guid>
    (fromPrimitiveCasting: CastOperator.Implicit,
        toPrimitiveCasting: CastOperator.Implicit)]
    public readonly partial struct OrderItemId
    {
        public static Validation Validate(Guid orderItemId)
        {
            return orderItemId == Guid.Empty ? Validation.Invalid("The OrderItemId cannot be an empty GUID.") : Validation.Ok;
        }
    }

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
}
