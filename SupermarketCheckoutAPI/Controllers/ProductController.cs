using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Application.Services;
using SupermarketCheckoutAPI.Filters;

namespace SupermarketCheckoutAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ModelStateErrorRequestFilter]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            try
            {
                await _productService.AddProductAsync(productDto);
                return Ok("Product added successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error occured.");
            }
        }

    }
}
