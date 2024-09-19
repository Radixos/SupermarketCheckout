namespace SupermarketCheckout.Model.Repositories
{
    public interface IItemCatalogRepository
    {
        Task<BasketItemPrice> GetBasketItemPriceBySKUAsync(char sku);
    }
}
