using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SupermarketCheckout.API.Filters;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Exceptions;

namespace SupermarketCheckout.API.Controllers
{
    [ApiController]
    [Route("api/products/{sku}/price")]
    [ModelStateErrorRequestFilter]
    public class ProductPriceController : Controller
    {
        private readonly IProductService _productService;

        public ProductPriceController(IProductService productService)
        {
            _productService = productService
                ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(string sku)
        {
            try
            {
                var price = await _productService.GetProductPriceAsync(sku);

                var response = price;   //TODO: Create an object and return it, also in other controllers too

                return Ok(response);
            }
            catch (Exception ex)
                when (ex is ArgumentException
                      or NotFoundException)
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

        [HttpPut]
        public async Task<ActionResult> PutAsync(string sku, decimal newPrice)
        {
            try
            {
                await _productService.UpdatePriceAsync(sku, newPrice);

                string? uri = Url.Action("Get", "Products", new { newPrice });

                return Accepted(uri, newPrice);
            }
            catch (Exception ex)
                when (ex is ArgumentException)
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
