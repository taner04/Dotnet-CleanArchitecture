using Domain.Entities.Products;

namespace Application.Common.Interfaces.Infrastructure.Repositories;

/// <summary>
/// Repository interface for managing <see cref="Product"/> entities.
/// </summary>
public interface IProductRepository : IRepository<Product, ProductId>
{
    /// <summary>
    /// Asynchronously retrieves a list of products matching the specified name.
    /// </summary>
    /// <param name="name">The name of the product to search for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of matching products.</returns>
    Task<List<Product>> GetByNameAsync(string name);
}