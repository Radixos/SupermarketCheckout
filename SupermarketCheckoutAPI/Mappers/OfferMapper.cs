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

        public static Offer? MapToOffer(OfferDto? offerDto)
        {
            if (offerDto == null)
            {
                return null;
            }

            return new Offer
            {
                Type = offerDto.Type,
                Price = offerDto.Price,
                Quantity = offerDto.Quantity
            };
        }

        public static OfferDto? MapToOfferDto(Offer? offer)
        {
            if (offer == null)
            {
                return null;
            }

            return new OfferDto
            {
                Type = offer.Type,
                Price = offer.Price,
                Quantity = offer.Quantity
            };
        }
    }
}
