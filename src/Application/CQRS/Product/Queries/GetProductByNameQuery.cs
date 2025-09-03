using Application.Abstraction;
using Application.Dtos.Product;
using Application.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Product.Queries;

public readonly record struct GetProductByNameQuery(string Name) : IQuery<ResultT<ProductDto>>;

public sealed class GetProductByNameQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetProductByNameQuery, ResultT<ProductDto>>
{
    public async ValueTask<ResultT<ProductDto>> Handle(GetProductByNameQuery query,
        CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{query.Name.ToLower()}%"))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (products is null)
        {
            return ErrorFactory.NotFound($"No products found with name containing '{query.Name}'.");
        }

        return products.ToProductDto();
    }
}

public sealed class GetProductByNameValidator : AbstractValidator<GetProductByNameQuery>
{
    public GetProductByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name cannot be empty.");
    }
}