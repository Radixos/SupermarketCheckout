using Moq;
using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IOfferRepository> _offerRepositoryMock;
        private ProductService _productService;

        [TestInitialize]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _offerRepositoryMock = new Mock<IOfferRepository>();
            _productService = new ProductService(_productRepositoryMock.Object,
                _offerRepositoryMock.Object);
        }

        [TestMethod]
        public void ProductService_ThrowsArgumentNullException_WhenProductRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductService(null, _offerRepositoryMock.Object));
        }

        [TestMethod]
        public void ProductService_ThrowsArgumentNullException_WhenOfferRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductService(_productRepositoryMock.Object, null));
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productService.GetProductAsync(null));
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productService.GetProductAsync(""));
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            var sku = "NonExistentSku";
            _productRepositoryMock
                .Setup(repo => repo.GetProductAsync(sku))
                .ReturnsAsync((Product)null);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                _productService.GetProductAsync(sku));
        }

        [TestMethod]
        public async Task GetProductAsync_ReturnsProductDto_WhenProductExists()
        {
            var sku = "ExistingSku";
            var product = new Product(sku, 100, null);
            var productDto = new ProductDto
            {
                Sku = sku,
                Price = 100
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductAsync(sku))
                .ReturnsAsync(product);

            var result = await _productService.GetProductAsync(sku);

            Assert.IsNotNull(result);
            Assert.AreEqual(productDto.Sku, result.Sku);
            Assert.AreEqual(productDto.Price, result.Price);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ThrowsNotFoundException_WhenNoProductsFound()
        {
            _productRepositoryMock
                .Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync((List<Product>)null);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                _productService.GetAllProductsAsync());
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ReturnsListOfProducts_WhenProductsExist()
        {
            var products = new List<Product>
            {
                new Product("Sku1", 50, null),
                new Product("Sku2", 100, null)
            };

            _productRepositoryMock
                .Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(products);

            var result = await _productService.GetAllProductsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Sku1", result[0].Sku);
            Assert.AreEqual(50m, result[0].Price);
            Assert.AreEqual("Sku2", result[1].Sku);
            Assert.AreEqual(100m, result[1].Price);
        }

        [TestMethod]
        public async Task AddProductAsync_ThrowsArgumentNullException_WhenProductDtoIsNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                _productService.AddProductAsync(null));
        }

        [TestMethod]
        public async Task AddProductAsync_AddsProductSuccessfully_WhenProductDtoIsValid()
        {
            var productDto = new ProductDto
            {
                Sku = "NewSku",
                Price = 100,
                Offer = null
            };

            await _productService.AddProductAsync(productDto);

            _productRepositoryMock.Verify(repo =>
                repo.AddProductAsync(It.Is<Product>(p =>
                    p.Sku == "NewSku" && p.Price == 100)), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProductAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productService.DeleteProductAsync(null));
        }

        [TestMethod]
        public async Task DeleteProductAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            var sku = "NonExistentSku";

            _productRepositoryMock
                .Setup(repo => repo.GetProductAsync(sku))
                .ReturnsAsync((Product)null);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                _productService.DeleteProductAsync(sku));
        }

        [TestMethod]
        public async Task DeleteProductAsync_DeletesProductSuccessfully_WhenProductExists()
        {
            var sku = "ExistingSku";
            var product = new Product(sku, 100, null);

            _productRepositoryMock
                .Setup(repo => repo.GetProductAsync(sku))
                .ReturnsAsync(product);

            await _productService.DeleteProductAsync(sku);

            _productRepositoryMock.Verify(repo =>
                repo.DeleteProductAsync(product), Times.Once);
        }
    }
}
