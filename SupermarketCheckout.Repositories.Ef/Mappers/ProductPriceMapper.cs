using SupermarketCheckout.Model;

namespace SupermarketCheckout.Repositories.Ef.Mappers
{
    public class ProductPriceMapper
    {
        public static ProductPrice MapToProductPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            return new ProductPrice(price);
        }
    }
}
