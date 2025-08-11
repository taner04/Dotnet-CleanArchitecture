using Application.Dtos.Product;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IProductService
    {
        Task<ResultT<List<ProductItemDto>>> GetAllAsync();
        Task<ResultT<List<ProductItemDto>>> SearchByNameAsync(ProductByNameDto productByNameDto);
    }
}
