namespace SupermarketCheckout.API.DTOs
{
    public class CheckoutRequest
    {
        public required List<string> Skus { get; set; }
    }
}
