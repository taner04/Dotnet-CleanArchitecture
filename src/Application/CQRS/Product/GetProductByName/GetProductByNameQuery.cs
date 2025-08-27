using Application.Dtos.Product;

namespace Application.CQRS.Product.GetProductByName;

public readonly record struct GetProductByNameQuery(string Name) : IQuery<ResultT<List<ProductDto>>>;