using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Mappers
{
    public class ProductPriceMapper
    {
        public static ProductPriceDto MapToProductPriceDto(ProductPrice productPrice)
        {
            if (productPrice == null)
            {
                throw new ArgumentNullException(nameof(productPrice));
            }

            return new ProductPriceDto
            {
                Price = productPrice.Price
            };
        }
    }
}
