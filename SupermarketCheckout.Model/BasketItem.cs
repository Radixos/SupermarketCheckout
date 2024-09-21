using SupermarketCheckout.Model.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketCheckout.Model
{
    public class BasketItem
    {
        public string SKU { get; }
        public int Quantity { get; }

        public BasketItem(string sku, int quantity)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            SKU = sku;
            Quantity = quantity;
        }

        public async Task<decimal> GetTotalPriceAsync(IItemCatalogRepository itemCatalogRepository)
        {
            if (itemCatalogRepository == null)
            {
                throw new ArgumentNullException(nameof(itemCatalogRepository));
            }

            var itemPrice = await itemCatalogRepository.GetBasketItemPriceBySKUAsync(SKU);

            if (itemPrice == null)
            {
                return 0;
            }

            return itemPrice.CalculateTotalBasketItemPrice(Quantity);
        }
    }
}
