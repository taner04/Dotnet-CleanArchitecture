using Application.Dtos.Cart;

namespace Application.CQRS.Cart.GetCart;

public readonly record struct GetCartByUserQuery : IQuery<ResultT<CartDto>>;