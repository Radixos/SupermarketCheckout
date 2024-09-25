using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef.Mappers
{
    internal static class BasketItemMapper
    {
        public static BasketItemPrice MapToBasketItemPrice(ProductEntity entity, IOfferFactory offerFactory)
        {
            if (entity == null)
            {
                return null;
            }

            return new BasketItemPrice(
                entity.Price,
                MapToOffer(entity.Offer, offerFactory));
        }

        public static Offer? MapToOffer(OfferEntity? entity, IOfferFactory offerFactory)
        {
            if (entity == null || !entity.OfferQuantity.HasValue || !entity.OfferPrice.HasValue)
            {
                return null;
            }

            return offerFactory.CreateOffer(
                entity.OfferType,
                entity.OfferQuantity.Value,
                entity.OfferPrice.Value);
        }
    }
}
