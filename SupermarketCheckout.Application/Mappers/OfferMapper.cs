using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Mappers
{
    public class OfferMapper
    {
        public static OfferDto MapToOfferDto(Offer offer)
        {
            return new OfferDto
            {
                OfferQuantity = offer.OfferQuantity,
                OfferPrice = offer.OfferPrice
            };
        }
    }
}
