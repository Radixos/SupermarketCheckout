using Microsoft.AspNetCore.Mvc;
using SupermarketCheckoutAPI.DTOs;
using SupermarketCheckout.Application.Services;

namespace SupermarketCheckoutAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost]
        public async Task<ActionResult<CheckoutResponseDto>> Post([FromBody] CheckoutRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(request);
            }

            if (string.IsNullOrWhiteSpace(request.SKUs))
            {
                return BadRequest(request.SKUs);
            }

            try
            {
                var totalPrice = await _checkoutService.GetTotalPriceAsync(request.SKUs);

                var response = new CheckoutResponseDto
                {
                    TotalPrice = totalPrice
                };

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
    }
}