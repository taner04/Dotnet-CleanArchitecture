using Domain.Entities.Products;

namespace Application.Common.Interfaces.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product, ProductId>
    {
        Task<List<Product>> GetByNameAsync(string name);
    }
}
