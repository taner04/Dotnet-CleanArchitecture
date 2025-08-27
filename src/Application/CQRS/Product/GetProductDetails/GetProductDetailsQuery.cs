using Application.Dtos.Product;

namespace Application.CQRS.Product.GetProductDetails;

public readonly record struct GetProductDetailsQuery(Guid ProductId) : IQuery<ResultT<ProductDto>>;