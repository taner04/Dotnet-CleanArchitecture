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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorFactory _validatorFactory;

        public ProductService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

        public async Task<ResultT<List<ProductDto>>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return ResultT<List<ProductDto>>.Success(products.Select(p => p.ToProductDto()).ToList());
        }

        public async Task<ResultT<ProductDto>> GetProductDetailsAsync(ProductId productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product is null)
            {
                return ResultT<ProductDto>.Failure(
                    ErrorFactory.NotFound($"Product with ID {productId} not found.")
                );
            }

            return ResultT<ProductDto>.Success(product.ToProductDto());
        }

        public async Task<ResultT<List<ProductDto>>> SearchByNameAsync(ProductByNameDto productByName)
        {
            var validationResult = _validatorFactory.GetResult(productByName);
            if (!validationResult.IsValid)
            {
                return ResultT<List<ProductDto>>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var products = await _unitOfWork.ProductRepository.GetByNameAsync(productByName.Name);
            return ResultT<List<ProductDto>>.Success(products.Select(p => p.ToProductDto()).ToList());
        }
    }
}
