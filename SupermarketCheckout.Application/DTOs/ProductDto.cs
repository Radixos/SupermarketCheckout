namespace SupermarketCheckout.Application.DTOs
{
    public class ProductDto
    {
        public required string Sku { get; set; }
        public decimal Price { get; set; }
        public OfferDto? Offer { get; set; }
    }
}
