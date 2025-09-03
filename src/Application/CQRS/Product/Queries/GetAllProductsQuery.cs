using Application.Abstraction;
using Application.Dtos.Product;
using Application.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Product.Queries;

public readonly record struct GetAllProductsQuery : IQuery<ResultT<List<ProductDto>>>;

public sealed class GetAllProductsQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAllProductsQuery, ResultT<List<ProductDto>>>
{
    public async ValueTask<ResultT<List<ProductDto>>> Handle(GetAllProductsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await dbContext.Products.ToListAsync(cancellationToken);
        return products.Select(p => p.ToProductDto()).ToList();
    }
}