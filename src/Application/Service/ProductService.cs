using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Product;
using Application.Mapper;
using Application.Response;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Service
{
    [ServiceInjection(typeof(IProductService), ScopeType.AddTransient)]
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<ResultT<List<ProductItemDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return ResultT<List<ProductItemDto>>.Success(products.Select(p => p.ToProductItemDto()).ToList());
        }

        public async Task<ResultT<List<ProductItemDto>>> SearchByNameAsync(string name)
        {
            var products = await _productRepository.SearchByNameAsync(name);
            return ResultT<List<ProductItemDto>>.Success(products.Select(p => p.ToProductItemDto()).ToList());
        }
    }
}
