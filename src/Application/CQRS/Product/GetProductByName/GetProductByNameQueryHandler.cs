using Application.Dtos.Product;
using Application.Mapper;

namespace Application.CQRS.Product.GetProductByName;

public sealed class GetProductByNameQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetProductByNameQuery, ResultT<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<List<ProductDto>>> Handle(GetProductByNameQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductRepository.GetByNameAsync(query.Name);
        return ResultT<List<ProductDto>>.Success([.. products.Select(p => p.ToProductDto())]);
    }
}