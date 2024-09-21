using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef.Mappers
{
    internal static class BasketItemMapper
    {
        public static BasketItemPrice MapToBasketItemPrice(BasketItemEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new BasketItemPrice(
                entity.Price,
                MapToOffer(entity.Offer));
        }

        public static Offer? MapToOffer(OfferEntity? entity)
        {
            if (entity == null || !entity.OfferQuantity.HasValue || !entity.OfferPrice.HasValue)
            {
                return null;
            }

            var offerFactory = new OfferFactory();
            return offerFactory.CreateOffer(
                entity.OfferType,
                entity.OfferQuantity.Value,
                entity.OfferPrice.Value);
        }
    }
}
