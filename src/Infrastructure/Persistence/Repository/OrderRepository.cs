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
            return await DbSet.Where(o => o.UserId == userId)
                              .Include(o => o.OrderItems)
                                  .ThenInclude(oi => oi.Product)
                              .ToListAsync();
        }
    }
}
