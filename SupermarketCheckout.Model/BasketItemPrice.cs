namespace SupermarketCheckout.Model
{
    public class BasketItemPrice
    {
        public BasketItemPrice(decimal unitPrice, Offer? offer)
        {
            if (unitPrice < 0) {
                throw new ArgumentOutOfRangeException(nameof(unitPrice));
            }

            UnitPrice = unitPrice;
            Offer = offer;
        }

        public decimal UnitPrice { get; }
        public Offer? Offer { get; }

        public bool HasOffer
        {
            get
            {
                return Offer != null;
            }
        }

        public decimal CalculateTotalBasketItemPrice(int skuQuantity)
        {
            if (skuQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skuQuantity));
            }

            if (HasOffer && Offer != null)
            {
                return Offer.CalculateOfferPrice(skuQuantity, UnitPrice);
            }

            return skuQuantity * UnitPrice;
        }
    }
}
