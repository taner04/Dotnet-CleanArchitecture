namespace Application.CQRS.Cart.RemoveCartItem;

public readonly record struct RemoveCartItemCommand(Guid UserId, Guid CartItemId) : ICommand<Result>;