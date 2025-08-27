namespace Application.Dtos.CartItem;

public readonly record struct RemoveCartItemDto(Guid UserId, Guid CartItemId);