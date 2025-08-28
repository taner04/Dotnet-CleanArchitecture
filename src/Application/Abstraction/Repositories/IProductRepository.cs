using Domain.Entities.Products;

namespace Application.Abstraction.Repositories;

public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<List<Product>> GetByNameAsync(string name);
}