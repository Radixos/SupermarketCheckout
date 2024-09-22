using SupermarketCheckout.Model;

namespace SupermarketCheckoutAPI.DTOs
{
    public class ProductDto
    {
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public Offer? Offer { get; set; } // Is it okay to link to domain model in DTO on API?
    }
}
