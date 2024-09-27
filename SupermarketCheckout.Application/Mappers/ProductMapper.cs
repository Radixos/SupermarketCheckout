using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Mappers
{
    public class ProductMapper
    {
        public static ProductDto MapToProductDto(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductDto
            {
                Sku = product.Sku,
                Price = product.Price,
                OfferType = product.OfferType
            };
        }
    }
}
