using Moq;
using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductPriceServiceTests
    {
        private Mock<IProductPriceRepository> _productPriceRepositoryMock;
        private ProductPriceService _productPriceService;

        [TestInitialize]
        public void SetUp()
        {
            _productPriceRepositoryMock = new Mock<IProductPriceRepository>();
            _productPriceService = new ProductPriceService(_productPriceRepositoryMock.Object);
        }

        [TestMethod]
        public void
            ProductPriceService_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductPriceService(null));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productPriceService.GetProductPriceAsync(null));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productPriceService.GetProductPriceAsync(string.Empty));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsWhitespace()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productPriceService.GetProductPriceAsync(" "));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            var sku = "NonExistentSku";
            _productPriceRepositoryMock
                .Setup(repo => repo.GetProductPriceAsync(sku))
                .ThrowsAsync(new NotFoundException("Product not found"));

            var service = new ProductPriceService(_productPriceRepositoryMock.Object);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                service.GetProductPriceAsync(sku));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ReturnsProductPrice_WhenProductExists()
        {
            var sku = "ExistingSku";
            var productPrice = new ProductPrice(50);
            var productPriceDto = new ProductPriceDto { Price = 50m };

            _productPriceRepositoryMock
                .Setup(repo => repo.GetProductPriceAsync(sku))
                .ReturnsAsync(productPrice);

            var result = await _productPriceService.GetProductPriceAsync(sku);

            Assert.IsNotNull(result);
            Assert.AreEqual(productPrice.Price, result.Price);
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productPriceService.UpdatePriceAsync(null, 10));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productPriceService.UpdatePriceAsync(string.Empty, 10));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsArgumentException_WhenSkuIsWhitespace()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _productPriceService.UpdatePriceAsync(" ", 10));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsArgumentOutOfRangeException_WhenPriceIsNegative()
        {
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
                _productPriceService.UpdatePriceAsync("A", -1));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            var sku = "NonExistentSku";

            _productPriceRepositoryMock
                .Setup(repo => repo.GetProductPriceAsync(sku))
                .ThrowsAsync(new NotFoundException());

            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                _productPriceService.UpdatePriceAsync(sku, 50));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_SuccessfullyUpdatesPrice_WhenValidSkuAndPrice()
        {
            var sku = "ExistingSku";
            var productPrice = new ProductPrice(50);

            _productPriceRepositoryMock
                .Setup(repo => repo.GetProductPriceAsync(sku))
                .ReturnsAsync(productPrice);

            _productPriceRepositoryMock
                .Setup(repo => repo.UpdatePriceAsync(It.IsAny<ProductPrice>(), sku))
                .Returns(Task.CompletedTask);

            await _productPriceService.UpdatePriceAsync(sku, 60);

            Assert.AreEqual(60, productPrice.Price);

            _productPriceRepositoryMock.Verify(repo =>
                repo.UpdatePriceAsync(It.Is<ProductPrice>(p => p.Price == 60), sku), Times.Once);
        }
    }
}
