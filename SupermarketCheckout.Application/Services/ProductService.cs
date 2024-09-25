using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
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

        public async Task<ProductDto> GetProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            var product = await _productRepository.GetProductAsync(sku);

            return ProductMapper.MapToProductDto(product);
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }

            var product = new Product(productDto.Sku, productDto.Price, productDto.OfferType ?? null);  //TODO ASK: Is this the right way? At what point should I break the object down?
            await product.AddProductAsync(_productRepository);
        }

        public async Task DeleteProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentNullException(nameof(sku));
            }

            await _productRepository.DeleteProductAsync(sku);
        }
    }
}
