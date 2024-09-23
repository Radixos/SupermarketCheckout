using System;

namespace SupermarketCheckout.Model
{
    public class OfferFactory : IOfferFactory
    {
        public Offer CreateOffer(string offerType, int offerQuantity, decimal offerPrice)   //TODO: write tests (offer type "MultiBuy", test lower case letters)
        {
            if (string.IsNullOrWhiteSpace(offerType)) {
                throw new ArgumentException("Cannot be null or white space", nameof(offerType));
            }

            if (offerQuantity <= 0) {
                throw new ArgumentOutOfRangeException(nameof(offerQuantity));
            }

            if (offerPrice < 0) {
                throw new ArgumentOutOfRangeException(nameof(offerPrice));
            }

            switch (offerType) {
                case "MultiBuy":
                    return new MultiBuyOffer(offerQuantity, offerPrice);
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(offerType),
                        $"OfferType {offerType} is invalid.");
            }
        }
    }
}