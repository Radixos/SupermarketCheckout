using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Filters;
using SupermarketCheckout.API.Mappers;

//TODO: Add relevant tests for all of this and deeper

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
            _productService = productService
                ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<ActionResult<ProductsResponse>> GetAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();

                var response = ProductMapper.MapToProductsResponse(products);

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

        [HttpGet("{sku}")]
        public async Task<ActionResult<Product>> GetAsync(string sku)
        {
            try
            {
                var product = await _productService.GetProductAsync(sku);

                var response = ProductMapper.MapToProduct(product);

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

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Product product)
        {
            try
            {
                await _productService.AddProductAsync(ProductMapper.MapToProductDto(product));

                string? uri = Url.Action("Get", "Products", new { product.Sku });

                return Created(uri, product.Sku);
            }
            catch (Exception ex)
                when (ex is ArgumentException
                      or InvalidOperationException)
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

        [HttpDelete("{sku}")]
        public async Task<ActionResult> DeleteAsync(string sku)
        {
            try
            {
                await _productService.DeleteProductAsync(sku);

                string? uri = Url.Action("Delete", "Products", new { sku });

                return Accepted(uri, sku);
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
    }
}
