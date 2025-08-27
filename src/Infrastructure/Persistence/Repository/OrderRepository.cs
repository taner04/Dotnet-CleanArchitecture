using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    internal class OrderRepository : Repository<Order, OrderId>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Order>> OrdersByUserAsync(UserId userId)
        {
            return DbSet.Where(order => order.UserId == userId)
                        .Include(order => order.OrderItems)
                            .ThenInclude(item => item.Product)
                        .ToListAsync();
        }
    }
}
