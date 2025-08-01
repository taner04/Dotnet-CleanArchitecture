using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Product;
using Application.Mapper;
using Application.Response;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Service
{
    [ServiceInjection(typeof(IProductService), ScopeType.AddScoped)]
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<List<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return Result<List<ProductDto>>.Success(products.Select(ProductMapper.ToProductDto).ToList());
        }
    }
}
