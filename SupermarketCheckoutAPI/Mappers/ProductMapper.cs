using SupermarketCheckout.Application.DTOs;
using SupermarketCheckoutAPI.DTOs;

namespace SupermarketCheckoutAPI.Mappers
{
    public class ProductMapper
    {
        public static ProductDto MapToProductDto(Product product)
        {
            return new ProductDto
            {
                SKU = product.SKU,
                Price = product.Price,
                OfferType = product.OfferType
            };
        }
    }
}
