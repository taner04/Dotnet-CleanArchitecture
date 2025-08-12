using Application.Dtos.Product;
using SharedKernel.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IProductService
    {
        Task<ResultT<List<ProductDto>>> GetAllAsync();
        Task<ResultT<List<ProductDto>>> SearchByNameAsync(ProductByNameDto productByNameDto);
        Task<ResultT<ProductDto>> GetProductDetailsAsync(ProductDetailsByIdDto productDetailsById);
    }
}
