using SupermarketCheckout.API.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class ProductPriceMapper
    {
        public static ProductPriceResponse MapToProductPriceResponse(decimal price)
        {
            return new ProductPriceResponse
            {
                Price = price
            };
        }
    }
}
