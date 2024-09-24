using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model
{
    public class Basket
    {
        private List<BasketItem> _basketItems { get; }

        public Basket(List<string> SKUs)
        {
            if (SKUs == null)
            {
                throw new ArgumentNullException(nameof(SKUs));
            }

            if (SKUs.Count < 1)
            {
                throw new ArgumentException("Cannot be null or white space", nameof(SKUs));
            }

            var itemsList = new Dictionary<string, int>();

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
