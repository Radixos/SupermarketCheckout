using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Filters;
using SupermarketCheckout.Model.Exceptions;

namespace SupermarketCheckout.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ModelStateErrorRequestFilter]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService
                ?? throw new ArgumentNullException(nameof(checkoutService));
        }

        [HttpPost]
        public async Task<ActionResult<CheckoutResponse>> PostAsync([FromBody][Required] CheckoutRequest request)
        {
            try
            {
                var totalPrice = await _checkoutService.GetTotalPriceAsync(request.Skus);

                var response = new CheckoutResponse //TODO: use a mapper
                {
                    TotalPrice = totalPrice
                };

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
    }
}