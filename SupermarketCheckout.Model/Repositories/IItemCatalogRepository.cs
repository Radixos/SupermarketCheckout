namespace SupermarketCheckout.Model.Repositories
{
    public interface IItemCatalogRepository
    {
        Task<BasketItemPrice> GetBasketItemPriceBySkuAsync(string sku);
    }
}
