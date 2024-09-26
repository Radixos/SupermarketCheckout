namespace SupermarketCheckout.Model.Repositories
{
    public interface IProductPriceRepository
    {
        Task<decimal> GetProductPriceAsync(string sku);
        Task UpdatePriceAsync(string sku, decimal newPrice);
    }
}
