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
        public async Task<ActionResult> GetAsync(string SKU)    //TODO: make SKUs lowercase
        {
            try
            {
                var response = await _productService.GetProductAsync(SKU);  //TODO: have product in dto on app and map to product on application

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            //catch (NotFoundException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return NotFound();
            //}
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
                string? uri = Url.Action("Get", "Products", new { product.SKU });

                return Created(uri, product.SKU);
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
