using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class OfferRepositoryTests
    {
        [TestMethod]
        public void
            OfferRepositoryController_ThrowsArgumentNullException_WhenContextIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new OfferRepository(null, GetOfferFactory()));
        }

        [TestMethod]
        public void
            OfferRepositoryController_ThrowsArgumentNullException_WhenOfferFactoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new OfferRepository(GetContext(), null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentNullException_WhenSkuIsNull()
        {
            var repo = GetOfferRepository();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync(null, "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentNullException_WhenSkuIsEmpty()
        {
            var repo = GetOfferRepository();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync("", "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentNullException_WhenOfferTypeIsNull()
        {
            var repo = GetOfferRepository();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync("A", null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentNullException_WhenOfferTypeIsEmpty()
        {
            var repo = GetOfferRepository();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync("A", ""));
        }

        private OfferRepository GetOfferRepository()
        {
            return new OfferRepository(GetContext(), GetOfferFactory());
        }

        private IOfferFactory GetOfferFactory()
        {
            return new OfferFactory();
        }

        private SupermarketContext GetContext()
        {
            return new SupermarketContext(new DbContextOptions<SupermarketContext>());
        }
    }
}
