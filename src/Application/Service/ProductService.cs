using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Product;
using Application.Mapper;
using Application.Response;
using Application.Validator;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Service
{
    [ServiceInjection(typeof(IProductService), ScopeType.AddTransient)]
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidatorFactory _validatorFactory;

        public ProductService(IProductRepository productRepository, IValidatorFactory validatorFactory)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

        public async Task<ResultT<List<ProductItemDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return ResultT<List<ProductItemDto>>.Success(products.Select(p => p.ToProductItemDto()).ToList());
        }

        public async Task<ResultT<List<ProductItemDto>>> SearchByNameAsync(ProductByNameDto productByName)
        {
            var validationResult = _validatorFactory.GetResult(productByName);
            if (!validationResult.IsValid)
            {
                return ResultT<List<ProductItemDto>>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var products = await _productRepository.SearchByNameAsync(productByName.Name);
            return ResultT<List<ProductItemDto>>.Success(products.Select(p => p.ToProductItemDto()).ToList());
        }
    }
}
