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

            var product = new Product(productDto.Sku, productDto.Price, productDto.OfferType ?? null);

            await _productRepository.AddProductAsync(product);  //TODO: finish
        }

        public async Task DeleteProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            //get product
            //mark product as deleted (done by domain model)
            //save the domain model

            await _productRepository.DeleteProductAsync(sku);
        }

        public async Task<decimal> GetProductPriceAsync(string sku) //TODO: move to ProductPriceService
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            return await _productRepository.GetProductPriceAsync(sku);  //Needs to return the domain model but service needs to return dto, so map
        }

        public async Task UpdatePriceAsync(string sku, decimal newPrice)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice));
            }

            //get prod price
            //update price
            //save

            await _productRepository.UpdatePriceAsync(sku, newPrice);
        }
    }
}
