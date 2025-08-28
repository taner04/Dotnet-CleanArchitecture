using Domain.Entities.Carts;

namespace Application.Abstraction.Repositories;

public interface ICartRepository : IRepository<Cart, CartId>
{
    Task<Cart?> GetCartByUserId(UserId userId);
}