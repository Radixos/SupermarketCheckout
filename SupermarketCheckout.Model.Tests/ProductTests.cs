using Moq;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Product_ThrowsArgumentException_WhenSkuIsNull()
        {
            Assert.ThrowsException<ArgumentException>(() => new Product(null, 50, null));
        }

        [TestMethod]
        public void Product_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            Assert.ThrowsException<ArgumentException>(() => new Product("", 50, null));
        }

        [TestMethod]
        public void Product_ThrowsArgumentOutOfRangeException_WhenPriceIsBelowZero()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Product("A", -1, null));
        }

        [TestMethod]
        public void Product_ConstructsSuccessfully_WithValidParameters()
        {
            var product = new Product("A", 50, null);
            Assert.AreEqual("A", product.Sku);
            Assert.AreEqual(50, product.Price);
            Assert.IsNull(product.Offer);
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            var product = new Product("A", 50, null);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
                await product.GetProductAsync(null));
        }

        [TestMethod]
        public async Task GetProductAsync_ReturnsProduct_WhenCalledWithValidRepository()
        {
            var product = new Product("A", 50, null);
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetProductAsync(It.IsAny<string>()))
                .ReturnsAsync(product);

            var result = await product.GetProductAsync(mockRepo.Object);

            Assert.AreEqual(product, result);
        }

    }
}
