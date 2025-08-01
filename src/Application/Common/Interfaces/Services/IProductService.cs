using Application.Dtos.Product;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> GetAllProductsAsync();
    }
}
