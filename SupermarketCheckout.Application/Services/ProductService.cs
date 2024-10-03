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
        private readonly IOfferRepository _offerRepository;

        public ProductService(IProductRepository productRepository, IOfferRepository offerRepository)
        {
            _productRepository = productRepository
                ?? throw new ArgumentNullException(nameof(productRepository));
            _offerRepository = offerRepository
                ?? throw new ArgumentNullException(nameof(offerRepository));
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            if (products == null)
            {
                throw new NotFoundException();
            }

            return ProductMapper.MapToProductsDto(products);
        }

        public async Task<ProductDto> GetProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Cannot be null or white space", nameof(sku));
            }

            var product = await _productRepository.GetProductAsync(sku);

            if (product == null)
            {
                throw new NotFoundException();
            }

            return ProductMapper.MapToProductDto(product);
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto));
            }

            var offerFactory = new OfferFactory();

            Offer? offer = null;

            if (productDto.Offer != null)
            {
                offer = offerFactory.CreateOffer(productDto.Offer.Type,
                    productDto.Offer.Quantity, productDto.Offer.Price);
            }

            var product = new Product(productDto.Sku, productDto.Price, offer);

            await _productRepository.AddProductAsync(product);
        }

        public async Task DeleteProductAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(nameof(sku));
            }

            var product = await _productRepository.GetProductAsync(sku);

            if (product == null)
            {
                throw new NotFoundException($"Product with SKU {sku} not found.");
            }

            //Call the domain model to perform any action when necessary

            await _productRepository.DeleteProductAsync(product);
        }
    }
}
