namespace Application.CQRS.Cart.AddCartItem;

public readonly record struct AddCartItemCommand(Guid ProductId, int Quantity) : ICommand<Result>;