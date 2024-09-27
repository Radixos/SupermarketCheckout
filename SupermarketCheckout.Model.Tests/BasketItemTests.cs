using Moq;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class BasketItemTests
    {
        [TestMethod]
        public void BasketItemController_ThrowsArgumentException_WhenSkuIsNull()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new BasketItem(null, 1));
        }

        [TestMethod]
        public void BasketItemController_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new BasketItem("", 1));
        }

        [TestMethod]
        public void BasketItemController_ThrowsArgumentOutOfRangeException_WhenQuantityIsZero()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BasketItem("A", 0));
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
                .Setup(m =>
                    m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()))
                .ReturnsAsync(new BasketItemPrice(50, null));

            await basketItem.GetTotalPriceAsync(repoMock.Object);

            repoMock.Verify(m =>
                m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()), Times.Once);
        }

        private BasketItem GetBasketItem()
        {
            return new BasketItem("A", 1);
        }
    }
}
