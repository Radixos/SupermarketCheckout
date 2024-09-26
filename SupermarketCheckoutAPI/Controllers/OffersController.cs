using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Filters;
using SupermarketCheckout.API.Mappers;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Exceptions;

namespace SupermarketCheckout.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ModelStateErrorRequestFilter]
    public class OffersController : Controller
    {
        private readonly IOfferService _offerService;

        public OffersController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet]
        public async Task<ActionResult<Offer>> GetAsync(string offerType)
        {
            try
            {
                var offer = await _offerService.GetOfferAsync(offerType);

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

        //[HttpPost]
        //public async Task<ActionResult> PostAsync([FromBody] Offer offer)
        //{
        //    try
        //    {
        //        await _offerService.AddOfferAsync(OfferMapper.MapToOfferDto(offer));

        //        string? uri = Url.Action("Get", "Products", new { offer.OfferType });

        //        return Created(uri, offer.OfferType);
        //    }
        //    catch (Exception ex)
        //        when (ex is ArgumentException)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(500, "Internal server error occured.");
        //    }
        //}
    }
}
