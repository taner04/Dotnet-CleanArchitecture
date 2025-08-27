using Application.Dtos.Product;
using Application.Mapper;

namespace Application.CQRS.Product.GetProductDetails;

public sealed class GetProductDetailsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetProductDetailsQuery, ResultT<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<ProductDto>> Handle(GetProductDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(query.ProductId);

        if (product is null)
            return ResultT<ProductDto>.Failure(
                ErrorFactory.NotFound($"Product with ID {query.ProductId} not found.")
            );

        return ResultT<ProductDto>.Success(product.ToProductDto());
    }
}