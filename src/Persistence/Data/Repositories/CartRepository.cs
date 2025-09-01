using Application.Abstraction.Repositories;
using Domain.Entities.Carts;
using Domain.ValueObjects.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repositories;

public sealed class CartRepository(ApplicationDbContext dbContext)
    : Repository<Cart, CartId>(dbContext), ICartRepository
{
    public Task<Cart?> GetCartByUserId(UserId userId)
    {
        return Queryable.Where(DbSet, c => c.UserId == userId)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync();
    }
}