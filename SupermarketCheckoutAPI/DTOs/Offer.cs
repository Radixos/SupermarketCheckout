namespace SupermarketCheckout.API.DTOs
{
    public class Offer
    {
        public required string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
