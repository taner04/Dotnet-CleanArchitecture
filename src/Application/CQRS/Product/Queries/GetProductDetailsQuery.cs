using Application.Dtos.Product;
using Application.Mapper;
using Application.Validator;

namespace Application.CQRS.Product.Queries;

public readonly record struct GetProductDetailsQuery(Guid ProductId) : IQuery<ResultT<ProductDto>>;

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