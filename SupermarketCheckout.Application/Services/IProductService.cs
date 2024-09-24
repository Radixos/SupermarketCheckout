using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Services
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(string SKU);
        Task AddProductAsync(ProductDto productDto);
    }
}
