using Castle.DynamicProxy.Internal;
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
        public void EnsureItemCatalogRepositoryIsSuppliedToTheConstructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CheckoutService(null));
        }

        [TestMethod]
        public async Task EnsureGetTotalPriceAsyncThrowsWhenSKUsIsNullAsync()
        {
            var service = GetCheckoutService();
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.GetTotalPriceAsync(null));
        }

        [TestMethod]
        public async Task EnsureGetTotalPriceAsyncThrowsWhenSKUsIsEmptyAsync()
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