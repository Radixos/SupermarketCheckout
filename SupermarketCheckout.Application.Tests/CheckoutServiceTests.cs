using Moq;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class CheckoutServiceTests
    {
        [TestMethod]
        public void CheckoutService_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CheckoutService(null));
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ThrowsArgumentNullException_WhenSkusIsNull_Async()
        {
            var service = GetCheckoutService();
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.GetTotalPriceAsync(null));
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ThrowsArgumentException_WhenSkusIsEmpty_Async()
        {
            var service = GetCheckoutService();
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => service.GetTotalPriceAsync(new List<string>()));
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ReturnsCorrectTotal_WhenSkusAreValid()
        {
            var skus = new List<string> { "A", "B" };
            var repositoryMock = new Mock<IItemCatalogRepository>();

            var itemPriceA = new BasketItemPrice(50, null);
            var itemPriceB = new BasketItemPrice(30, null);

            repositoryMock.Setup(r => r.GetBasketItemPriceBySkuAsync("A")).ReturnsAsync(itemPriceA);
            repositoryMock.Setup(r => r.GetBasketItemPriceBySkuAsync("B")).ReturnsAsync(itemPriceB);

            var service = new CheckoutService(repositoryMock.Object);

            var totalPrice = await service.GetTotalPriceAsync(skus);

            Assert.AreEqual(80, totalPrice);
        }

        private CheckoutService GetCheckoutService()
        {
            return new CheckoutService(GetItemCatalogRepository());
        }

        private IItemCatalogRepository GetItemCatalogRepository()
        {
            return new Mock<IItemCatalogRepository>().Object;
        }
    }
}
