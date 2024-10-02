using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model
{
    public class ProductPrice
    {
        public decimal Price { get; private set; }

        public ProductPrice(decimal price)
        {
            Price = price;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice));
            }

            if (Price == newPrice)
            {
                throw new ArgumentException($"Price is already set to {newPrice}");
            }

            Price = newPrice;
        }
    }
}