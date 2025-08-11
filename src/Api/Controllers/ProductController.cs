using Application.Dtos.Product;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productService.GetAllAsync();
            return MapResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetProductByName([FromBody] ProductByNameDto productByName)
        {
            var result = await _productService.SearchByNameAsync(productByName);
            return MapResponse(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductDetailsAsync([FromRoute] ProductDetailsByIdDto productId)
        {
            var result = await _productService.GetProductDetailsAsync(ProductId.From(productId.ProductId));
            return MapResponse(result);
        }
    }
}
