using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model;
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
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice));
            }

            var productPrice = await _productPriceRepository.GetProductPriceAsync(sku); //get product price

            productPrice.UpdatePrice(newPrice); //update price

            await _productPriceRepository.UpdatePriceAsync(productPrice, sku); //save
        }
    }
}
