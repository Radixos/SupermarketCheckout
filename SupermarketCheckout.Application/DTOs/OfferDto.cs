namespace SupermarketCheckout.Application.DTOs
{
    public class OfferDto
    {
        public required string Type { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
