using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class OfferMapper
    {
        public static OfferResponse MapToOffer(OfferDto offer)
        {
            return new OfferResponse
            {
                OfferPrice = offer.OfferPrice,
                OfferQuantity = offer.OfferQuantity
            };
        }
    }
}
