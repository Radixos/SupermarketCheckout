using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Mappers
{
    public class ProductPriceMapper
    {
        public static ProductPriceDto MapToProductPriceDto(ProductPrice productPriceDto)
        {
            return new ProductPriceDto
            {
                Price = productPriceDto.Price
            };
        }
    }
}
