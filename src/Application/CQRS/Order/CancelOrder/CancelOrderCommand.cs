namespace Application.CQRS.Order.CancelOrder;

public readonly record struct CancelOrderCommand(Guid UserId, Guid OrderId) : ICommand<Result>;