using Microsoft.AspNetCore.Mvc;
using SupermarketCheckoutAPI.DTOs;
using SupermarketCheckoutAPI.Filters;

namespace SupermarketCheckoutAPI.Controllers
{
    [ApiController]
    [Route("api/products/{sku}/price")]
    [ModelStateErrorRequestFilter]
    public class ProductsPriceController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetAsync(string SKU)    //TODO: finish a method to return a price of a single product
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(string SKU, decimal newPrice)
        {
            try
            {
                throw new NotImplementedException();
                return Accepted();
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
