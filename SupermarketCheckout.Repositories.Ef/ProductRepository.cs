using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
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

            var product = await _context.BasketItem
                .Select(basketItem => new
                {
                    basketItem.SKU,
                    basketItem.Price,
                    OfferType = basketItem.Offer != null ? basketItem.Offer.OfferType : null
                })
                .FirstOrDefaultAsync(p => p.SKU == sku);

            if (product == null)
            {
                throw new InvalidOperationException($"Product with SKU '{sku}' already exists.");
            }

            return new Product(product.SKU, product.Price, product.OfferType);
        }

        public async Task AddProductAsync(string sku, decimal price, string? offerType = null)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            OfferEntity? offerEntity = null;

            if (!string.IsNullOrWhiteSpace(offerType))
            {
                offerEntity = await _context.Offer.FirstOrDefaultAsync(o => o.OfferType == offerType);
            }

            var newProduct = new BasketItemEntity() //TODO Ask: Shouldnt I merge BasketItem and Product together on the Model?
            {
                SKU = sku,
                Price = price,
                OfferId = offerEntity?.OfferId,
                Offer = offerEntity
            };

            _context.BasketItem.Add(newProduct);
            await _context.SaveChangesAsync();
        }
    }
}
