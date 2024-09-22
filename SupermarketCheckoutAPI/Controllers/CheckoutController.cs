using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SupermarketCheckoutAPI.DTOs;
using SupermarketCheckout.Application.Services;
using SupermarketCheckoutAPI.Filters;

/*
TODO: Do I need to move the tests after introducing OfferFactory? Or add specific to OfferFactory?
TODO: Add method to add data to the db
TODO: Automate db setting up process
*/

namespace SupermarketCheckoutAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ModelStateErrorRequestFilter]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost]
        public async Task<ActionResult<CheckoutResponseDto>> Post([FromBody][Required] CheckoutRequestDto request)
        {
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