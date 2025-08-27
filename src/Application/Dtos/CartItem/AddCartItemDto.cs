namespace Application.Dtos.CartItem;

public readonly record struct AddCartItemDto(Guid UserId, Guid ProductId, int Quantity);