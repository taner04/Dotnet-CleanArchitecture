namespace Application.CQRS.Cart.AddCartItem;

public readonly record struct AddCartItemCommand(Guid UserId, Guid ProductId, int Quantity) : ICommand<Result>;