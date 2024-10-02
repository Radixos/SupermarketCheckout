using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef.Mappers;

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

        public async Task<ProductPrice> GetProductPriceAsync(string sku)
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

            return ProductPriceMapper.MapToProductPrice(product.Price);
        }

        public async Task UpdatePriceAsync(ProductPrice productPrice, string sku)
        {
            if (productPrice == null)
            {
                throw new ArgumentNullException(nameof(productPrice));
            }

            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(sku);
            }

            var product = await _context.Product.FirstOrDefaultAsync(p => p.Sku == sku);

            if (product == null)
            {
                throw new NotFoundException(nameof(product));
            }

            product.Price = productPrice.Price;

            await _context.SaveChangesAsync();
        }
    }
}
