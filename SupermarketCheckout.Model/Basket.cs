using SupermarketCheckout.Model.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SupermarketCheckout.Model
{
    public class Basket
    {
        private List<BasketItem> _basketItems { get; }

        public Basket(string SKUs)
        {
            if (string.IsNullOrWhiteSpace(SKUs))
            {
                throw new ArgumentException(nameof(SKUs));
            }

            var itemsList = new Dictionary<char, int>();

            foreach (var sku in SKUs)
            {
                if (!itemsList.ContainsKey(sku))
                {
                    itemsList[sku] = 0;
                }
                itemsList[sku]++;
            }

            _basketItems = itemsList.Select(i => new BasketItem(i.Key, i.Value)).ToList();
        }

        public async Task<decimal> GetTotalPriceAsync(IItemCatalogRepository itemCatalogRepository)
        {
            if (itemCatalogRepository == null)
            {
                throw new ArgumentNullException(nameof(itemCatalogRepository));
            }

            decimal totalPrice = 0;

            foreach (var itemEntry in _basketItems)
            {
                totalPrice += await itemEntry.GetTotalPriceAsync(itemCatalogRepository);
            }

            return totalPrice;
        }
    }
}
