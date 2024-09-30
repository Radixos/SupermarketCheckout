using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class OfferMapper
    {
        public static OfferResponse? MapToOfferResponse(OfferDto? offer)
        {
            if (offer == null)
            {
                return null;
            }

            return new OfferResponse
            {
                OfferPrice = offer.Price,
                OfferQuantity = offer.Quantity
            };
        }

        public static Offer? MapToOffer(OfferDto? productDtoOffer)
        {
            if (productDtoOffer == null)
            {
                return null;
            }

            return new Offer
            {
                Type = productDtoOffer.Type,
                Price = productDtoOffer.Price,
                Quantity = productDtoOffer.Quantity
            };
        }

        public static OfferDto? MapToOfferDto(Offer? productOffer)
        {
            if (productOffer == null)
            {
                return null;
            }

            return new OfferDto
            {
                Type = productOffer.Type,
                Price = productOffer.Price,
                Quantity = productOffer.Quantity
            };
        }
    }
}
