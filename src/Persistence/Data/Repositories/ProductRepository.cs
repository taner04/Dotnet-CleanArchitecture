using Application.Abstraction.Repositories;
using Domain.Entities.Products;
using Domain.ValueObjects.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repositories;

public sealed class ProductRepository(ApplicationDbContext dbContext)
    : Repository<Product, ProductId>(dbContext), IProductRepository
{
    public async Task<List<Product>> GetByNameAsync(string name)
    {
        return await DbSet.Where(p => EF.Functions.ILike(p.Name, $"%{name}%"))
            .ToListAsync();
    }
}