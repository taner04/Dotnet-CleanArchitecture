namespace Application.CQRS.Cart.RemoveCartItem;

public readonly record struct RemoveCartItemCommand(Guid CartItemId) : ICommand<Result>;