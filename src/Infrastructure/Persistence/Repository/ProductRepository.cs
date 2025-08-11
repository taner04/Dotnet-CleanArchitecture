using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    [ServiceInjection(typeof(IProductRepository), ScopeType.AddTransient)]
    public sealed class ProductRepository : Repository<Product, ProductId>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public Task<List<Product>> SearchByNameAsync(string name)
        {
            return DbSet.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                        .ToListAsync();
        }
    }
}
