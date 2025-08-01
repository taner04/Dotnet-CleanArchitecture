using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    [ServiceInjection(typeof(IOrderRepository), ScopeType.AddTransient)]
    public sealed class OrderRepository : Repository<Order, OrderId>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Order>> GetAllByUserIdAsync(UserId userId)
        {
            return await DbSet.Include(o => o.OrderItems)
                              .Where(o => o.UserId.Value == userId.Value)
                              .ToListAsync();
        }

        public async Task<Order?> GetOrderToCancel(OrderId orderId, UserId userId)
        {
            return await DbSet.Include(o => o.OrderItems)
                              .FirstOrDefaultAsync(o => o.Id.Value == orderId.Value && o.UserId.Value == userId.Value);
        }
    }
}
