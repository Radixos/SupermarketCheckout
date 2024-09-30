namespace SupermarketCheckout.API.DTOs
{
    public class Product
    {
        public required string Sku { get; set; }
        public decimal Price { get; set; }
        public Offer? Offer { get; set; }
    }
}
