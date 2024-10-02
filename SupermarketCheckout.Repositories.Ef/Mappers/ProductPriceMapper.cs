using SupermarketCheckout.Model;

namespace SupermarketCheckout.Repositories.Ef.Mappers
{
    public class ProductPriceMapper
    {
        public static ProductPrice MapToProductPrice(decimal price)
        {
            return new ProductPrice(price);
        }
    }
}
