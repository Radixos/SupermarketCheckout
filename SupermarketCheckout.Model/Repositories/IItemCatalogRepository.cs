namespace SupermarketCheckout.Model.Repositories
{
    public interface IItemCatalogRepository
    {
        Task<BasketItemPrice> GetBasketItemPriceBySKUAsync(string sku);
    }
}
