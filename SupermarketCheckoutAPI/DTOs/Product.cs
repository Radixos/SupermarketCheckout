namespace SupermarketCheckout.API.DTOs
{
    public class Product
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string? OfferType { get; set; }
    }
}
