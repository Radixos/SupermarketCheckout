using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Filters;
using SupermarketCheckout.API.Mappers;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Exceptions;

namespace SupermarketCheckout.API.Controllers
{
    [ApiController]
    [Route("api/products/{sku}/offers")]
    [ModelStateErrorRequestFilter]
    public class ProductOffersController : Controller
    {
        private readonly IProductOfferService _productOfferService;

        public ProductOffersController(IProductOfferService productOfferService) //What else to rename, change?
        {
            _productOfferService = productOfferService
                ?? throw new ArgumentNullException(nameof(productOfferService));
        }

        [HttpGet("{offerType}")]
        public async Task<ActionResult<Offer>> GetAsync(string sku, string offerType)   //TODO: finish
        {
            try
            {
                var offer = await _productOfferService.GetOfferAsync(offerType);

                var response = OfferMapper.MapToOffer(offer);

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
