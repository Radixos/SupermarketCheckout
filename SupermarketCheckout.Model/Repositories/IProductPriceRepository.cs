namespace SupermarketCheckout.Model.Repositories
{
    public interface IProductPriceRepository
    {
        Task<ProductPrice> GetProductPriceAsync(string sku);
        Task UpdatePriceAsync(string sku, decimal newPrice);
    }
}
