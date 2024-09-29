using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.Application.Services
{
    public interface IProductPriceService
    {
        Task<ProductPriceDto> GetProductPriceAsync(string sku);
        Task UpdatePriceAsync(string sku, decimal newPrice);
    }
}
