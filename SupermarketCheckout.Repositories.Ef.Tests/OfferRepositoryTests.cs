using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;
using System.Threading.Tasks;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class OfferRepositoryTests
    {
        private DbContextOptions<SupermarketContext> _dbContextOptions;

        [TestInitialize]
        public void Initialize()
        {
            // Setup in-memory database for each test
            _dbContextOptions = new DbContextOptionsBuilder<SupermarketContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [TestMethod]
        public void OfferRepository_ThrowsArgumentNullException_WhenContextIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new OfferRepository(null, GetOfferFactory()));
        }

        [TestMethod]
        public void OfferRepository_ThrowsArgumentNullException_WhenOfferFactoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new OfferRepository(GetContext(), null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            var repo = GetOfferRepository();
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync(null, "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            var repo = GetOfferRepository();
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync("", "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenOfferTypeIsNull()
        {
            var repo = GetOfferRepository();
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync("A", null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenOfferTypeIsEmpty()
        {
            var repo = GetOfferRepository();
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetOfferAsync("A", ""));
        }

        [TestMethod]
        public async Task GetOfferAsync_ReturnsOffer_WhenOfferExists()
        {
            // Arrange
            var sku = "A";
            var offerType = "MultiBuy";
            var offerEntity = new OfferEntity
            {
                OfferId = 1,
                OfferType = offerType,
                OfferQuantity = 2,
                OfferPrice = 10,
                BasketItems = new List<ProductEntity>() // Initialize BasketItems
            };

            using (var context = GetContext())
            {
                context.Offer.Add(offerEntity);
                context.Product.Add(new ProductEntity
                {
                    Sku = sku,
                    Price = 50,
                    OfferId = offerEntity.OfferId
                });
                await context.SaveChangesAsync();
            }

            OfferRepository repo;
            Offer result;
            using (var context = GetContext())
            {
                repo = new OfferRepository(context, GetOfferFactory());
                result = await repo.GetOfferAsync(sku, offerType);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(offerType, result.OfferType);
            Assert.AreEqual(2, result.OfferQuantity);
            Assert.AreEqual(10, result.OfferPrice);
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
            return new SupermarketContext(_dbContextOptions);
        }
    }
}
