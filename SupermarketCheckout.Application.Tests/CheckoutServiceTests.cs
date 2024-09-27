using Moq;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class CheckoutServiceTests
    {
        [TestMethod]
        public void CheckoutServiceController_ThrowsArgumentNullException_WhenRepositoryIsNull()
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