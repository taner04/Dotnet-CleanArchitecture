using Application.Abstraction.Repositories;
using Domain.Entities.Orders;
using Domain.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repository;

internal class OrderRepository(ApplicationDbContext dbContext)
    : Repository<Order, OrderId>(dbContext), IOrderRepository
{
    public Task<List<Order>> OrdersByUserAsync(UserId userId)
    {
        return DbSet.Where(order => order.UserId == userId)
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.Product)
            .ToListAsync();
    }
}