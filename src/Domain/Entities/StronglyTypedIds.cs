using Vogen;

namespace Domain.Entities
{
    [ValueObject<int>]
    public readonly partial struct JwtId;

    [ValueObject<int>]
    public readonly partial struct OrderId;

    [ValueObject<int>]
    public readonly partial struct OrderItemId;

    [ValueObject<int>]
    public readonly partial struct ProductId;

    [ValueObject<int>]
    public readonly partial struct UserId;
}
