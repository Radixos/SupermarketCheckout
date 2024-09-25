using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductAsync(string sku);
        Task AddProductAsync(ProductDto productDto);
    }
}
