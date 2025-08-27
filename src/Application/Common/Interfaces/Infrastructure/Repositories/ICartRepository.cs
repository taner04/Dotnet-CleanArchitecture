using Domain.Entities.Carts;

namespace Application.Common.Interfaces.Infrastructure.Repositories;

public interface ICartRepository : IRepository<Cart, CartId>
{
    Task<Cart?> GetCartByUserId(UserId userId);
}