using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.Application.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductAsync(string sku);
        Task AddProductAsync(ProductDto productDto);
        Task DeleteProductAsync(string sku);
    }
}
