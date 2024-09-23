using System;

namespace SupermarketCheckout.Model
{
    public abstract class Offer
    {
        protected Offer(int offerQuantity, decimal offerPrice)  //TODO: Write new tests
        {
            if (offerQuantity <= 0) {
                throw new ArgumentOutOfRangeException(nameof(offerQuantity));
            }

            if (offerPrice <= 0) {
                throw new ArgumentOutOfRangeException(nameof(offerPrice));
            }

            OfferQuantity = offerQuantity;
            OfferPrice = offerPrice;
        }

        public int OfferQuantity { get; }

        public decimal OfferPrice { get; }

        public abstract decimal CalculateOfferPrice(int skuQuantity, decimal unitPrice);
    }
}