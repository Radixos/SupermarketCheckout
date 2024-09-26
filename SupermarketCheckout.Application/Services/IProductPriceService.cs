namespace SupermarketCheckout.Application.Services
{
    public interface IProductPriceService
    {
        Task<decimal> GetProductPriceAsync(string sku);
        Task UpdatePriceAsync(string sku, decimal newPrice);
    }
}
