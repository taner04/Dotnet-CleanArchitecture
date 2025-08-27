using Application.Dtos.CartItem;

namespace Application.Dtos.Cart;

public readonly record struct CartDto(Guid CartId, List<CartItemDto> CartItems);