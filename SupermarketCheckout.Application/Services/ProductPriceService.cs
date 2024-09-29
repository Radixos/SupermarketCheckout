using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Services
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly IProductPriceRepository _productPriceRepository;

        public ProductPriceService(IProductPriceRepository productPriceRepository)
        {
            _productPriceRepository = productPriceRepository
                ?? throw new ArgumentNullException(nameof(productPriceRepository));
        }

        public async Task<ProductPriceDto> GetProductPriceAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku)) {
                throw new ArgumentException(nameof(sku));
            }

            var productPrice = await _productPriceRepository.GetProductPriceAsync(sku);

            return ProductPriceMapper.MapToProductPriceDto(productPrice);
        }

        public async Task UpdatePriceAsync(string sku, decimal newPrice)
        {
            if (string.IsNullOrWhiteSpace(sku)) {
                throw new ArgumentException(nameof(sku));
            }

            if (newPrice < 0) {
                throw new ArgumentOutOfRangeException(nameof(newPrice));
            }

            //get prod price
            //update price
            //save

            //TODO ASK: How can I apply above comments? The way I see this work would be like below but I'd need to make
            //an extra call to a different repo:

            //var product = await _productPriceRepository.GetProductBySkuAsync(sku);
            //if (product == null) {
            //    throw new NotFoundException($"Product with SKU {sku} not found.");
            //}

            //product.Price = newPrice;

            //await _productPriceRepository.UpdateProductAsync(product);

            await _productPriceRepository.UpdatePriceAsync(sku, newPrice);
        }
    }
}
