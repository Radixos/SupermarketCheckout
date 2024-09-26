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

        public async Task AddProductAsync(Product product)  //pass in a new model, do the validation for existing obj there
        {
            if (string.IsNullOrWhiteSpace(product.Sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(product.Sku));
            }

            if (product.Price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(product.Price));
            }

            var existingProduct = await _context.Product.FirstOrDefaultAsync(p => p.Sku == product.Sku);

            if (existingProduct != null)
            {
                throw new InvalidOperationException(nameof(existingProduct));
            }

            OfferEntity? offerEntity = null;

            if (!string.IsNullOrWhiteSpace(product.OfferType))
            {
                offerEntity = await _context.Offer.FirstOrDefaultAsync(o => o.OfferType == product.OfferType);
            }

            var newProduct = new ProductEntity
            {
                Sku = product.Sku,
                Price = product.Price,
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
    }
}
