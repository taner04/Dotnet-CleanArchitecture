using Domain.Entities.Carts;
using CartId = Domain.ValueObjects.Identifiers.CartId;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.Abstraction.Repositories;

public interface ICartRepository : IRepository<Cart, CartId>
{
    Task<Cart?> GetCartByUserId(UserId userId);
}