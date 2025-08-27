namespace Application.Dtos.Order;

public readonly record struct CancelOrderRequest(Guid UserId, Guid OrderId);