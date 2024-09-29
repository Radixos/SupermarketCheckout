using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
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

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return ProductMapper.MapToProductsDto(products);
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

            await _productRepository.AddProductAsync(product);  //TODO ASK: can this receive a domain model?
        }

        public async Task DeleteProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            //get product
            //mark product as deleted (done by domain model)    but how to od that? and why?
            //save the domain model

            //var product = await _productRepository.GetProductBySkuAsync(sku);
            //if (product == null)
            //{
            //    throw new NotFoundException($"Product with SKU {sku} not found.");
            //}

            //product.MarkAsDeleted(); //will have to adjust the db but then what's the point in deleting if I have a flag on the product?

            //await _productRepository.UpdateProductAsync(product);

            await _productRepository.DeleteProductAsync(sku);
        }
    }
}
