using SupermarketCheckout.Model;

namespace SupermarketCheckoutAPI.DTOs
{
    public class Product
    {
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public string OfferType { get; set; } // Is it okay to link to domain model in DTO on API? previously was
                                              // public Offer offer { get; set; }
    }
}
