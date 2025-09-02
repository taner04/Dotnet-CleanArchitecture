using Application.Dtos.Product;
using Application.Mapper;

namespace Application.CQRS.Product.Queries;

public readonly record struct GetProductByNameQuery(string Name) : IQuery<ResultT<List<ProductDto>>>;

public sealed class GetProductByNameQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetProductByNameQuery, ResultT<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<ResultT<List<ProductDto>>> Handle(GetProductByNameQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductRepository.GetByNameAsync(query.Name);
        return products.Select(p => p.ToProductDto()).ToList();
    }
}

public sealed class GetProductByNameValidator : AbstractValidator<GetProductByNameQuery>
{
    public GetProductByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters.");
    }
}