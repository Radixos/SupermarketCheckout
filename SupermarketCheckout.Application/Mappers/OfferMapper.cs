using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Mappers
{
    public class OfferMapper
    {
        public static OfferDto? MapToOfferDto(Offer? productOffer)
        {
            if (productOffer == null)
            {
                return null;
            }

            return new OfferDto
            {
                Type = productOffer.OfferType,
                Price = productOffer.OfferPrice,
                Quantity = productOffer.OfferQuantity
            };
        }
    }
}
