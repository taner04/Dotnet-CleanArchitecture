using Application.Abstraction.Utils;
using Application.Dtos.Product;
using Application.Mapper;
using Domain.ValueObjects.Identifiers;

namespace Application.CQRS.Product.GetProductDetails;

public sealed class GetProductDetailsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetProductDetailsQuery, ResultT<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<ProductDto>> Handle(GetProductDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var productId = ProductId.From(query.ProductId);
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

        if (product is null)
        {
            ErrorFactory.NotFound($"Product with ID {query.ProductId} not found.");
        }

        return ResultT<ProductDto>.Success(product.ToProductDto());
    }
}