using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository
                ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public Task AddProductAsync(ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
