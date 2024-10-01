using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Filters;
using SupermarketCheckout.API.Mappers;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Exceptions;

namespace SupermarketCheckout.API.Controllers
{
    [ApiController]
    [Route("api/products/{sku}/price")]
    [ModelStateErrorRequestFilter]
    public class ProductPriceController : Controller
    {
        private readonly IProductPriceService _productPriceService;

        public ProductPriceController(IProductPriceService productService)
        {
            _productPriceService = productService
                ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string sku)
        {
            try
            {
                var productPrice = await _productPriceService.GetProductPriceAsync(sku);

                var response = ProductPriceMapper.MapToProductPriceResponse(productPrice);

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
        public async Task<IActionResult> PutAsync(string sku, decimal newPrice)
        {
            try
            {
                await _productPriceService.UpdatePriceAsync(sku, newPrice);

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
