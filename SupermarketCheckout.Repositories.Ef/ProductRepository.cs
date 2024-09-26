using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef
{
    public class ProductRepository : IProductRepository
    {
        private readonly SupermarketContext _context;

        public ProductRepository(SupermarketContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> GetProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            var product = await _context.Product
                .Select(product => new
                {
                    product.Sku,
                    product.Price,
                    OfferType = product.Offer != null ? product.Offer.OfferType : null
                })
                .FirstOrDefaultAsync(p => p.Sku == sku);

            if (product == null)
            {
                throw new NotFoundException($"Product with Sku '{sku}' doesn't exists.");
            }

            return new Product(product.Sku, product.Price, product.OfferType);
        }

        public async Task AddProductAsync(string sku, decimal price, string? offerType = null)  //pass in a new model, do the validation for existing obj there
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            var product = await _context.Product.FirstOrDefaultAsync(p => p.Sku == sku);

            if (product != null)
            {
                throw new InvalidOperationException(nameof(product));
            }

            OfferEntity? offerEntity = null;

            if (!string.IsNullOrWhiteSpace(offerType))
            {
                offerEntity = await _context.Offer.FirstOrDefaultAsync(o => o.OfferType == offerType);
            }

            var newProduct = new ProductEntity
            {
                Sku = sku,
                Price = price,
                OfferId = offerEntity?.OfferId,
                Offer = offerEntity
            };

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            var product = await _context.Product.FirstOrDefaultAsync(p => p.Sku == sku);

            if (product == null)
            {
                throw new NotFoundException(nameof(product));
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetProductPriceAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(p => p.Sku == sku);

            if (product == null)
            {
                throw new NotFoundException(nameof(product));
            }

            return product.Price;
        }

        public async Task UpdatePriceAsync(string sku, decimal newPrice)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice));
            }

            var product = await _context.Product.FirstOrDefaultAsync(p => p.Sku == sku);

            if (product == null)
            {
                throw new NotFoundException(nameof(product));
            }

            if (product.Price == newPrice)
            {
                throw new ArgumentException($"Price is already set to {newPrice}");
            }

            product.Price = newPrice;

            await _context.SaveChangesAsync();
        }
    }
}
