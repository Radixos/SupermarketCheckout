using System.Diagnostics;

namespace SupermarketCheckout.Model
{
    public abstract class Offer
    {
        public static Offer CreateOffer(string offerType, int offerQuantity, decimal offerPrice)
        {
            if (string.IsNullOrWhiteSpace(offerType))
            {
                throw new ArgumentException(nameof(offerType));
            }

            if (offerQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offerQuantity));
            }

            if (offerPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offerPrice));
            }

            switch (offerType)
            {
                case "MultiBuy":
                    return new MultiBuyOffer(offerQuantity, offerPrice);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(offerType),
                        $"OfferType {offerType} is invalid.");

            }
        }

        protected Offer(int offerQuantity, decimal offerPrice)
        {
            if (offerQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offerQuantity));
            }

            if (offerPrice <= 0)
            {
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
