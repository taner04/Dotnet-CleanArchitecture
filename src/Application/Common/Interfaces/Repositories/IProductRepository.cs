using Domain.Entities.Products;

namespace Application.Common.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product, ProductId>
    {
        Task<List<Product>> SearchByName(string name);
    }
}
