using Application.Abstraction;
using Application.Dtos.Product;
using Application.Mapper;
using Application.Validator;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Product.Queries;

public readonly record struct GetProductDetailsQuery(Guid ProductId) : IQuery<ResultT<ProductDto>>;

public sealed class GetProductDetailsQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetProductDetailsQuery, ResultT<ProductDto>>
{
    public async ValueTask<ResultT<ProductDto>> Handle(GetProductDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var productId = ProductId.From(query.ProductId);
        
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

        if (product is null)
        {
            return ErrorFactory.NotFound($"Product with ID {query.ProductId} not found.");
        }

        return product.ToProductDto();
    }
}

public sealed class GetProductDetailsQueryValidator : AbstractValidator<GetProductDetailsQuery>
{
    public GetProductDetailsQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}