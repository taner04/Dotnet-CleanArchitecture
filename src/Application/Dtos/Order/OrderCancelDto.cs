namespace Application.Dtos.Order
{
    public readonly record struct OrderCancelDto(UserId UserId, OrderId OrderId);
}
