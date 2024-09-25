using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.API.Filters;

namespace SupermarketCheckout.API.Controllers
{
    [ApiController]
    [Route("api/products/{sku}/price")]
    [ModelStateErrorRequestFilter]
    public class ProductsPriceController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> GetAsync(string sku)    //TODO: finish a method to return a price of a single product
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(string sku, decimal newPrice)
        {
            try
            {
                throw new NotImplementedException();
                return Accepted();
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
