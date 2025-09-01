using Domain.Entities.Products;
using ProductId = Domain.ValueObjects.Identifiers.ProductId;

namespace Application.Abstraction.Repositories;

public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<List<Product>> GetByNameAsync(string name);
}