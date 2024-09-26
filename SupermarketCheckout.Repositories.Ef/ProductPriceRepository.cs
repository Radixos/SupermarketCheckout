using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Repositories.Ef
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly SupermarketContext _context;

        public ProductPriceRepository(SupermarketContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
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
