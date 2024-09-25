using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model
{
    public class BasketItem
    {
        public string Sku { get; }
        public int Quantity { get; }

        public BasketItem(string sku, int quantity)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            Sku = sku;
            Quantity = quantity;
        }

        public async Task<decimal> GetTotalPriceAsync(IItemCatalogRepository itemCatalogRepository)
        {
            if (itemCatalogRepository == null)
            {
                throw new ArgumentNullException(nameof(itemCatalogRepository));
            }

            var itemPrice = await itemCatalogRepository.GetBasketItemPriceBySkuAsync(Sku);

            if (itemPrice == null)
            {
                return 0;
            }

            return itemPrice.CalculateTotalBasketItemPrice(Quantity);
        }
    }
}
