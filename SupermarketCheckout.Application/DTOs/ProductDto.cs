namespace SupermarketCheckout.Application.DTOs
{
    public class ProductDto
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string? OfferType { get; set; }
    }
}
