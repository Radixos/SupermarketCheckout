using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef.Mappers
{
    public class ProductMapper
    {
        public static Product MapToProduct(dynamic productEntity)
        {
            return new Product(
                productEntity.Sku,
                productEntity.Price,
                productEntity.OfferType ?? null
            );
        }
    }
}
