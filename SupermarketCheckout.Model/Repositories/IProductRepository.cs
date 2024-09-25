namespace SupermarketCheckout.Model.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(string sku);
        Task AddProductAsync(string sku, decimal price, string? offerType);
        Task DeleteProductAsync(string sku);
    }
}
