using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Model;
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

        public async Task<Product> GetProductAsync(string SKU)
        {
            if (string.IsNullOrWhiteSpace(SKU))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(SKU));
            }

            return await _productRepository.GetProductAsync(SKU);
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }

            var product = new Product(productDto.SKU, productDto.Price, productDto.OfferType ?? null);  //TODO ASK: Is this the right way? At what point should I break the object down?
            await product.AddProductAsync(_productRepository);
        }
    }
}
