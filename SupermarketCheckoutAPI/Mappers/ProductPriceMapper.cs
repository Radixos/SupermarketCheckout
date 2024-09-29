using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Mappers
{
    public class ProductPriceMapper
    {
        public static ProductPriceResponse MapToProductPriceResponse(ProductPriceDto productPriceDto)
        {
            return new ProductPriceResponse
            {
                Price = productPriceDto.Price
            };
        }
    }
}
