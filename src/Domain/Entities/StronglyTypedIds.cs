using Vogen;

namespace Domain.Entities
{
    [ValueObject<Guid>]
    public readonly partial struct JwtId;

    [ValueObject<Guid>]
    public readonly partial struct OrderId;

    [ValueObject<Guid>]
    public readonly partial struct OrderItemId;

    [ValueObject<Guid>]
    public readonly partial struct ProductId;

    [ValueObject<Guid>]
    public readonly partial struct UserId;
}
