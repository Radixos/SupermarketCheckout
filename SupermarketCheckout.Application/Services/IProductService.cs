using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.Application.Services
{
    public interface IProductService
    {
        Task AddProductAsync(ProductDto productDto);    // Should I have separate DTOs per each layer?
    }
}
