using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Filters;
using SupermarketCheckout.API.Mappers;

//TODO: Add relevant tests for all this and deeper

namespace SupermarketCheckout.API.Controllers
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
        public async Task<ActionResult> GetAsync(string sku)
        {
            try
            {
                var response = await _productService.GetProductAsync(sku);  //TODO: have product in dto on app and map to product on application

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
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
                string? uri = Url.Action("Get", "Products", new { product.Sku });

                return Created(uri, product.Sku);
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
        public async Task<ActionResult> DeleteAsync(string sku) //TODO: Finish a method to delete a product
        {
            throw new NotImplementedException();

            return Ok();
        }
    }
}
