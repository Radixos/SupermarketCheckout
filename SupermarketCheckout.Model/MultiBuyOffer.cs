namespace SupermarketCheckout.Model
{
    public class MultiBuyOffer : Offer
    {
        public MultiBuyOffer(int offerQuantity, decimal offerPrice)
        : base(offerQuantity, offerPrice)
        { }

        public override decimal CalculateOfferPrice(int skuQuantity, decimal unitPrice)
        {
            if (skuQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skuQuantity));
            }

            if (unitPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unitPrice));
            }

            var offerCount = skuQuantity / OfferQuantity;
            var remainderCount = skuQuantity % OfferQuantity;

            return (offerCount * OfferPrice) + (remainderCount * unitPrice);
        }
    }
}
