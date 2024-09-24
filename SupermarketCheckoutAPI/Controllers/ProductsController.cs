using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Application.Services;
using SupermarketCheckoutAPI.DTOs;
using SupermarketCheckoutAPI.Filters;
using SupermarketCheckoutAPI.Mappers;

//TODO: Add relevant tests for all this and deeper

namespace SupermarketCheckoutAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ModelStateErrorRequestFilter]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(string SKU)
        {
            try
            {
                var response = await _productService.GetProductAsync(SKU); 
                // 1. Can I use the Dto on the application layer in a way: App -> Api?
                // 2. Do I need to map the dto on the API layer?
                return Ok(response);
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

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Product product)
        {
            try
            {
                await _productService.AddProductAsync(ProductMapper.MapToProductDto(product));

                return Created();
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

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string SKU) //TODO: Finish a method to delete a product
        {
            throw new NotImplementedException();

            return Ok();
        }
    }
}
