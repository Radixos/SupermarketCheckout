using Moq;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class BasketItemTests
    {
        [TestMethod]
        public void BasketItem_ThrowsArgumentException_WhenSkuIsNull()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new BasketItem(null, 1));
        }

        [TestMethod]
        public void BasketItem_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new BasketItem("", 1));
        }

        [TestMethod]
        public void BasketItem_ThrowsArgumentOutOfRangeException_WhenQuantityIsZero()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BasketItem("A", 0));
        }

        [TestMethod]
        public void BasketItem_DoesNotThrowException_WhenSkuAndQuantityAreValid()
        {
            var basketItem = new BasketItem("A", 1);
            Assert.AreEqual("A", basketItem.Sku);
            Assert.AreEqual(1, basketItem.Quantity);
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ThrowsArgumentNullException_WhenRepositoryIsNotSupplied_Async()
        {
            var basket = GetBasketItem();

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
                await basket.GetTotalPriceAsync(null));
        }

        [TestMethod]
        public async Task EnsureRepoIsCalledOnceForEachBasketItemAsync()
        {
            var basketItem = GetBasketItem();

            var repoMock = new Mock<IItemCatalogRepository>();
            repoMock
                .Setup(m => m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()))
                .ReturnsAsync(new BasketItemPrice(50, null));

            await basketItem.GetTotalPriceAsync(repoMock.Object);

            repoMock.Verify(m =>
                m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task EnsureRepoIsCalledOnceWithCorrectSkuAsync()
        {
            var basketItem = GetBasketItem();

            var repoMock = new Mock<IItemCatalogRepository>();
            repoMock
                .Setup(m => m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()))
                .ReturnsAsync(new BasketItemPrice(50, null));

            await basketItem.GetTotalPriceAsync(repoMock.Object);

            repoMock.Verify(m => m.GetBasketItemPriceBySkuAsync("A"), Times.Once);
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ReturnsZero_WhenItemPriceIsNull_Async()
        {
            var basketItem = GetBasketItem();

            var repoMock = new Mock<IItemCatalogRepository>();
            repoMock
                .Setup(m => m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()))
                .ReturnsAsync((BasketItemPrice)null);

            var totalPrice = await basketItem.GetTotalPriceAsync(repoMock.Object);

            Assert.AreEqual(0, totalPrice);
        }

        private BasketItem GetBasketItem()
        {
            return new BasketItem("A", 1);
        }
    }
}
