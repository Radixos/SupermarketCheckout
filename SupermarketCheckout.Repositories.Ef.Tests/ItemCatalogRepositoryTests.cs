using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class ItemCatalogRepositoryTests
    {
        [TestMethod]
        public void ItemCatalogRepository_ThrowsArgumentNullException_WhenContextIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ItemCatalogRepository(null, new OfferFactory()));
        }

        [TestMethod]
        public void ItemCatalogRepository_ThrowsArgumentNullException_WhenOfferFactoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ItemCatalogRepository(GetSupermarketContext(), null));
        }

        [TestMethod]
        public async Task GetBasketItemPriceBySkuAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            var repo = GetItemCatalogRepository();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetBasketItemPriceBySkuAsync(null));
        }

        [TestMethod]
        public async Task GetBasketItemPriceBySkuAsync_ThrowsArgumentException_WhenSkuIsWhiteSpace()
        {
            var repo = GetItemCatalogRepository();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetBasketItemPriceBySkuAsync(" "));
        }

        [TestMethod]
        public async Task GetBasketItemPriceBySkuAsync_ReturnsBasketItemPrice_WhenSkuExists()
        {
            var sku = "A";
            var productEntity = new ProductEntity {
                Sku = sku,
                Price = 50,
                OfferId = null,
                Offer = null
            };

            var options = new DbContextOptionsBuilder<SupermarketContext>()
                .UseInMemoryDatabase(databaseName: "GetBasketItemPriceBySkuAsync_ReturnsBasketItemPrice_WhenSkuExists")
                .Options;

            using (var context = new SupermarketContext(options)) {
                context.Product.Add(productEntity);
                await context.SaveChangesAsync();
            }

            BasketItemPrice result;
            using (var context = new SupermarketContext(options)) {
                var repo = new ItemCatalogRepository(context, GetOfferFactory());
                result = await repo.GetBasketItemPriceBySkuAsync(sku);
            }

            Assert.IsNotNull(result);
            Assert.AreEqual(50, result.UnitPrice);
            Assert.IsFalse(result.HasOffer);
        }

        [TestMethod]
        public async Task GetBasketItemPriceBySkuAsync_ThrowsNotFoundException_WhenSkuDoesNotExist()
        {
            // Arrange
            var repo = GetItemCatalogRepository();
            var invalidSku = "NonExistingSku";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                repo.GetBasketItemPriceBySkuAsync(invalidSku));
        }

        private ItemCatalogRepository GetItemCatalogRepository()
        {
            return new ItemCatalogRepository(GetSupermarketContext(), GetOfferFactory());
        }

        private SupermarketContext GetSupermarketContext()
        {
            var options = new DbContextOptionsBuilder<SupermarketContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            return new SupermarketContext(options);
        }

        private IOfferFactory GetOfferFactory()
        {
            return new OfferFactory();
        }
    }
}
