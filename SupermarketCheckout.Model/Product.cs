using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model
{
    public class Product
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public Offer? Offer { get; set; }

        public Product(string sku, decimal price, Offer? offer)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            Sku = sku;
            Price = price;
            Offer = offer;
        }

        public async Task<Product> GetProductAsync(IProductRepository productRepository)
        {
            if (productRepository == null)
            {
                throw new ArgumentNullException(nameof(productRepository));
            }

            return await productRepository.GetProductAsync(Sku);
        }
    }
}
