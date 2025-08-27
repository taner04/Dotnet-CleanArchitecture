using Application.Dtos.Product;
using SharedKernel.Response;
using SharedKernel.Response.Results;

namespace Application.Common.Interfaces.Services;

/// <summary>
/// Service interface for product.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>A list of <see cref="ProductDto"/> wrapped in a <see cref="ResultT{T}"/>.</returns>
    Task<ResultT<List<ProductDto>>> GetAllAsync();

    /// <summary>
    /// Searches for products by name.
    /// </summary>
    /// <param name="productByNameDto">DTO containing the product name to search for.</param>
    /// <returns>A list of matching <see cref="ProductDto"/> wrapped in a <see cref="ResultT{T}"/>.</returns>
    Task<ResultT<List<ProductDto>>> SearchByNameAsync(ProductByNameDto productByNameDto);

    /// <summary>
    /// Gets detailed information for a product by its ID.
    /// </summary>
    /// <param name="productDetailsById">DTO containing the product ID.</param>
    /// <returns>The <see cref="ProductDto"/> details wrapped in a <see cref="ResultT{T}"/>.</returns>
    Task<ResultT<ProductDto>> GetProductDetailsAsync(ProductDetailsByIdDto productDetailsById);
}