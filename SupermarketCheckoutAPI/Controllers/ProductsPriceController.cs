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
    public class ProductsPriceController : Controller
    {
        private readonly IProductService _productService;

        public ProductsPriceController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(string sku)    //TODO: finish a method to return a price of a single product
        {
            try
            {
                var price = await _productService.GetProductPriceAsync(sku);

                var response = price;   //Do I need to do this? To maintain the decoupling thing?

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
