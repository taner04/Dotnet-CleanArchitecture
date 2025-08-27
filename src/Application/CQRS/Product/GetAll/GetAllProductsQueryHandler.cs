using Application.Dtos.Product;
using Application.Mapper;

namespace Application.CQRS.Product.GetAll;

public sealed class GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetAllProductsQuery, ResultT<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<List<ProductDto>>> Handle(GetAllProductsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        return ResultT<List<ProductDto>>.Success(products.Select(p => p.ToProductDto()).ToList());
    }
}