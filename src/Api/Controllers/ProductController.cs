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
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var result = await _productService.GetAllProductsAsync();
            return MapResponse(result);
        }
    }
}
