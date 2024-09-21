using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IItemCatalogRepository _itemCatalogRepository;

        public CheckoutService(IItemCatalogRepository itemCatalogRepository)
        {
            _itemCatalogRepository = itemCatalogRepository
                ?? throw new ArgumentNullException(nameof(itemCatalogRepository));
        }

        public async Task<decimal> GetTotalPriceAsync(List<string> SKUs)
        {
            if (SKUs == null)
            {
                throw new ArgumentNullException(nameof(SKUs));
            }

            var basket = new Basket(SKUs);
            return await basket.GetTotalPriceAsync(_itemCatalogRepository);
        }
    }
}
