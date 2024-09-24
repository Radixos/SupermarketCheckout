using SupermarketCheckout.Model;

namespace SupermarketCheckoutAPI.DTOs
{
    public class Product
    {
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public string? OfferType { get; set; }
    }
}
