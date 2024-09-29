namespace SupermarketCheckout.Model.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(string sku);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(string sku);
    }
}
