using Moq;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        public void Basket_ThrowsArgumentNullException_WhenSkusIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Basket(null));
        }

        [TestMethod]
        public void Basket_ThrowsArgumentException_WhenSkusIsEmpty()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Basket(new List<string>()));
        }

        [TestMethod]
        public void Basket_DoesNotThrowException_WhenSkusAreValid()
        {
            var basket = new Basket(new List<string> { "A", "B", "C" });
            Assert.IsNotNull(basket);
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ThrowsArgumentNullException_WhenRepositoryIsNotSupplied_Async()
        {
            var basket = GetBasket();
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                basket.GetTotalPriceAsync(null));
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ReturnsCorrectTotalPrice()
        {
            var repoMock = new Mock<IItemCatalogRepository>();
            repoMock.Setup(r => r.GetBasketItemPriceBySkuAsync("A"))
                .ReturnsAsync(new BasketItemPrice(50, null));
            repoMock.Setup(r => r.GetBasketItemPriceBySkuAsync("B"))
                .ReturnsAsync(new BasketItemPrice(30, null));

            var basket = new Basket(new List<string> { "A", "B", "A" });

            var totalPrice = await basket.GetTotalPriceAsync(repoMock.Object);

            Assert.AreEqual(130, totalPrice);
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ReturnsZero_WhenItemPriceIsNull()
        {
            var repoMock = new Mock<IItemCatalogRepository>();
            repoMock.Setup(r => r.GetBasketItemPriceBySkuAsync(It.IsAny<string>()))
                .ReturnsAsync((BasketItemPrice)null);

            var basket = new Basket(new List<string> { "A", "B" });

            var totalPrice = await basket.GetTotalPriceAsync(repoMock.Object);

            Assert.AreEqual(0, totalPrice);
        }

        private Basket GetBasket()
        {
            return new Basket(new List<string> { "A", "B", "C", "D" });
        }
    }
}
