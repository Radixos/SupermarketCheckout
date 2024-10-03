using SupermarketCheckout.API.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class CheckoutResponseMapper
    {
        public static CheckoutResponse MapToCheckoutResponse(decimal totalPrice)
        {
            if (totalPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalPrice));
            }

            return new CheckoutResponse
            {
                TotalPrice = totalPrice
            };
        }
    }
}
