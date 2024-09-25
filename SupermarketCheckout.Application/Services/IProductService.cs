using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductAsync(string sku);
        Task AddProductAsync(ProductDto productDto);
        Task DeleteProductAsync(string sku);
        Task<decimal> GetProductPriceAsync(string sku);
        Task UpdatePriceAsync(string sku, decimal newPrice);
    }
}
