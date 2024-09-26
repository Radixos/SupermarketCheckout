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

        public async Task<decimal> GetProductPriceAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku)) {
                throw new ArgumentException(nameof(sku));
            }

            return await _productPriceRepository.GetProductPriceAsync(sku);  //Needs to return the domain model but service needs to return dto, so map
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

            await _productPriceRepository.UpdatePriceAsync(sku, newPrice);
        }
    }
}
