using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductOfferServiceTests
    {
        [TestMethod]
        public void
            ProductOfferServiceController_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductOfferService(null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenSkuIsNull_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync(null, "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenSkuIsEmpty_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync("", "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenOfferTypeIsNull_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync("A", null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenOfferTypeIsEmpty_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync("A", ""));
        }

        private ProductOfferService GetProductOfferService()
        {
            return new ProductOfferService(new OfferRepository(
                new SupermarketContext(new DbContextOptions<SupermarketContext>()),
                new OfferFactory()));
        }
    }
}
