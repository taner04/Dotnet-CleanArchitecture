using Application.Dtos.Product;

namespace Application.CQRS.Product.GetAll;

public readonly record struct GetAllProductsQuery() : IQuery<ResultT<List<ProductDto>>>;