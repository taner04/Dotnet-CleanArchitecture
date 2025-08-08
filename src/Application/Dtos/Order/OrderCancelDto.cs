namespace Application.Dtos.Order
{
    public readonly record struct OrderCancelDto(Guid UserId, Guid OrderId);
}
