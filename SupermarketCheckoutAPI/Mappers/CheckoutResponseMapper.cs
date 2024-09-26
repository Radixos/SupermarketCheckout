using SupermarketCheckout.API.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class CheckoutResponseMapper
    {
        public static CheckoutResponse MapToCheckoutResponse(decimal totalPrice)
        {
            return new CheckoutResponse
            {
                TotalPrice = totalPrice
            };
        }
    }
}
