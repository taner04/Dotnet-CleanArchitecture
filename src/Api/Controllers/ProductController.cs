using Application.CQRS.Product.Queries;
using Application.Dtos.Product;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/products")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return MapResponse(result);
    }

    [HttpPost("name")]
    public async Task<IActionResult> GetProductByName([FromBody] GetProductByNameQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return MapResponse(result);
    }

    [HttpPost("details")]
    public async Task<IActionResult> GetProductDetailsAsync([FromBody] GetProductDetailsQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return MapResponse(result);
    }
}