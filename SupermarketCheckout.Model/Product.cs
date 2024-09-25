using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model
{
    public class Product
    {
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public string? OfferType { get; set; }

        public Product(string sku, decimal price, string? offerType = null)
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
            OfferType = offerType;
        }

        public async Task<Product> GetProductAsync(IProductRepository productRepository)
        {
            if (productRepository == null)
            {
                throw new ArgumentNullException(nameof(productRepository));
            }

            return await productRepository.GetProductAsync(Sku);
        }

        public async Task AddProductAsync(IProductRepository productRepository)
        {
            if (productRepository == null)
            {
                throw new ArgumentNullException(nameof(productRepository));
            }

            await productRepository.AddProductAsync(Sku, Price, OfferType);
        }
    }
}
