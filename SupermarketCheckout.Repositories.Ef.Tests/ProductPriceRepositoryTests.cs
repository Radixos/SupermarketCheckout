using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class ProductPriceRepositoryTests
    {
        private DbContextOptions<SupermarketContext> _options;

        [TestInitialize]
        public void SetUp()
        {
            var dbName = Guid.NewGuid().ToString();
            _options = new DbContextOptionsBuilder<SupermarketContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [TestMethod]
        public void ProductPriceRepositoryConstructor_ThrowsArgumentNullException_WhenContextIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ProductPriceRepository(null));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            var repo = new ProductPriceRepository(new SupermarketContext(_options));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetProductPriceAsync(null));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            var repo = new ProductPriceRepository(new SupermarketContext(_options));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.GetProductPriceAsync(string.Empty));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            using var context = new SupermarketContext(_options);
            var repo = new ProductPriceRepository(context);
            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                repo.GetProductPriceAsync("NonExistingSku"));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ReturnsCorrectPrice_WhenProductExists()
        {
            using (var context = new SupermarketContext(_options)) {
                // Seed a product into the in-memory database
                var product = new ProductEntity
                {
                    Sku = "ExistingSku",
                    Price = 50
                };
                context.Product.Add(product);
                await context.SaveChangesAsync();
            }

            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductPriceRepository(context);
                var result = await repo.GetProductPriceAsync("ExistingSku");
                Assert.IsNotNull(result);
                Assert.AreEqual(50, result.Price);
            }
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsArgumentNullException_WhenProductPriceIsNull()
        {
            var repo = new ProductPriceRepository(new SupermarketContext(_options));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                repo.UpdatePriceAsync(null, "SomeSku"));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            var repo = new ProductPriceRepository(new SupermarketContext(_options));
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                repo.UpdatePriceAsync(new ProductPrice(100), null));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            using var context = new SupermarketContext(_options);
            var repo = new ProductPriceRepository(context);
            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                repo.UpdatePriceAsync(new ProductPrice(100), "NonExistingSku"));
        }

        [TestMethod]
        public async Task UpdatePriceAsync_UpdatesPrice_WhenProductExists()
        {
            using (var context = new SupermarketContext(_options)) {
                var product = new ProductEntity
                {
                    Sku = "UpdateSku",
                    Price = 50
                };
                context.Product.Add(product);
                await context.SaveChangesAsync();
            }

            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductPriceRepository(context);
                var newPrice = new ProductPrice(75);

                await repo.UpdatePriceAsync(newPrice, "UpdateSku");

                var updatedProduct = await repo.GetProductPriceAsync("UpdateSku");
                Assert.AreEqual(75m, updatedProduct.Price);
            }
        }
    }
}
