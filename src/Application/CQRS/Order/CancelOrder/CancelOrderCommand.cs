namespace Application.CQRS.Order.CancelOrder;

public readonly record struct CancelOrderCommand(Guid OrderId) : ICommand<Result>;