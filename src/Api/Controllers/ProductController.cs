using Application.CQRS.Product.GetAll;
using Application.CQRS.Product.GetProductByName;
using Application.CQRS.Product.GetProductDetails;
using Application.Dtos.Product;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var result = await _mediator.Send(new GetAllProductsQuery());
        return MapResponse(result);
    }

    [HttpPost("name")]
    public async Task<IActionResult> GetProductByName([FromBody] GetProductByNameQuery query)
    {
        var result = await _mediator.Send(query);
        return MapResponse(result);
    }

    [HttpPost("details")]
    public async Task<IActionResult> GetProductDetailsAsync([FromBody] GetProductDetailsQuery query)
    {
        var result = await _mediator.Send(query);
        return MapResponse(result);
    }
}