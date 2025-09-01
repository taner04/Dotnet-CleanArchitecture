using Application.Abstraction.Repositories;
using Domain.Entities.Orders;
using Domain.ValueObjects.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repositories;

internal class OrderRepository(ApplicationDbContext dbContext)
    : Repository<Order, OrderId>(dbContext), IOrderRepository
{
    public Task<List<Order>> OrdersByUserAsync(UserId userId)
    {
        return Queryable.Where(DbSet, order => order.UserId == userId)
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.Product)
            .ToListAsync();
    }
}