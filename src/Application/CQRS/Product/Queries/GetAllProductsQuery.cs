using Application.Dtos.Product;
using Application.Mapper;

namespace Application.CQRS.Product.Queries;

public readonly record struct GetAllProductsQuery() : IQuery<ResultT<List<ProductDto>>>;

public sealed class GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetAllProductsQuery, ResultT<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<List<ProductDto>>> Handle(GetAllProductsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        return products.Select(p => p.ToProductDto()).ToList();
    }
}