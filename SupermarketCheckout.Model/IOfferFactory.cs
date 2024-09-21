using SupermarketCheckout.Model;

public interface IOfferFactory
{
    Offer CreateOffer(string offerType, int offerQuantity, decimal offerPrice);
}