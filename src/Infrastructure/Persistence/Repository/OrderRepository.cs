using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

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