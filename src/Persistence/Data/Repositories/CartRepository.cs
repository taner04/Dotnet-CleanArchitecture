using Application.Abstraction.Repositories;
using Domain.Entities.Carts;
using Domain.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repository;

public sealed class CartRepository(ApplicationDbContext dbContext)
    : Repository<Cart, CartId>(dbContext), ICartRepository
{
    public Task<Cart?> GetCartByUserId(UserId userId)
    {
        return DbSet.Where(c => c.UserId == userId)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync();
    }
}