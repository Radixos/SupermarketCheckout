using Microsoft.AspNetCore.Mvc;
using SupermarketCheckoutAPI.DTOs;
using SupermarketCheckoutAPI.Mappers;

namespace SupermarketCheckoutAPI.Controllers
{
    [Route("api/products/{sku}/price")]
    public class ProductsPriceController : Controller
    {
        // TODO: Add a get to return a price of one product
        [HttpPut]
        public async Task<ActionResult> PutAsync(string SKU, [FromBody] Product product)
        {
            try
            {
                //Add app call
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
