using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public sealed class ProductRepository : Repository<Product, ProductId>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<Product>> GetByNameAsync(string name)
        {
            return await DbSet.Where(p => EF.Functions.ILike(p.Name, $"%{name}%"))
                              .ToListAsync();
        }

    }
}
