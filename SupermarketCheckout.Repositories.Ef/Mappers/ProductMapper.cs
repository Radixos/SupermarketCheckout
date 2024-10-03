using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef.Mappers
{
    public class ProductMapper
    {
        public static Product MapToProduct(ProductEntity productEntity)
        {
            if (productEntity == null)
            {
                throw new ArgumentNullException(nameof(productEntity));
            }

            Offer? offer = null;

            if (productEntity.Offer != null)
            {
                var offerType = productEntity.Offer.OfferType;
                var offerQuantity = productEntity.Offer.OfferQuantity ?? 0;
                var offerPrice = productEntity.Offer.OfferPrice ?? 0;

                offer = new OfferFactory().CreateOffer(offerType, offerQuantity, offerPrice);
            }

            return new Product(
                productEntity.Sku,
                productEntity.Price,
                offer
            );
        }
    }
}
