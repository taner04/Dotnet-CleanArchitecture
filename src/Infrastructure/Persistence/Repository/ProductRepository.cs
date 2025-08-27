using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

public sealed class ProductRepository(ApplicationDbContext dbContext)
    : Repository<Product, ProductId>(dbContext), IProductRepository
{
    public async Task<List<Product>> GetByNameAsync(string name)
    {
        return await DbSet.Where(p => EF.Functions.ILike(p.Name, $"%{name}%"))
            .ToListAsync();
    }
}