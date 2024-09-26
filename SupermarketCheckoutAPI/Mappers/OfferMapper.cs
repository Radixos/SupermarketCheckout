using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class OfferMapper
    {
        public static Offer MapToOffer(OfferDto offer)
        {
            return new Offer
            {
                OfferPrice = offer.OfferPrice,
                OfferQuantity = offer.OfferQuantity
            };
        }
    }
}
