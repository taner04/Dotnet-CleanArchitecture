using Application.Dtos.Product;

namespace Application.Dtos.CartItem;

public readonly record struct CartItemDto(Guid CartItemId, int Quantity, ProductCartDto Product);