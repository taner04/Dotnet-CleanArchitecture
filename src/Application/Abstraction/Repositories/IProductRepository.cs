using Domain.Entities.Products;
using ProductId = Domain.ValueObjects.Identifiers.ProductId;

namespace Application.Abstraction.Repositories;

/// <summary>
/// Defines a contract for a repository that manages <see cref="Product"/> entities.
/// </summary>
public interface IProductRepository : IRepository<Product, ProductId>
{
    /// <summary>
    /// Asynchronously retrieves a list of products that match the specified name.
    /// </summary>
    /// <param name="name">The name of the products to search for.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of <see cref="Product"/> entities with the specified name.
    /// </returns>
    Task<List<Product>> GetByNameAsync(string name);
}