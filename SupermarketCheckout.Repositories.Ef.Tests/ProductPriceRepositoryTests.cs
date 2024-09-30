using Microsoft.EntityFrameworkCore;
using Moq;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef.Entities;
using System.Linq.Expressions;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class ProductPriceRepositoryTests
    {
        [TestMethod]
        public void
            ProductPriceRepositoryConstructor_ThrowsArgumentNullException_WhenContextIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductPriceRepository(null));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                GetProductPriceRepository().GetProductPriceAsync(null));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                GetProductPriceRepository().GetProductPriceAsync(""));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<SupermarketContext>()
                .UseInMemoryDatabase(databaseName: "SupermarketTestDB_NotFound")
                .Options;

            var context = new SupermarketContext(options);

            var repo = new ProductPriceRepository(context);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                repo.GetProductPriceAsync("NonExistingSku"));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ReturnsCorrectPrice_WhenProductExists()
        {
            var options = new DbContextOptionsBuilder<SupermarketContext>()
                .UseInMemoryDatabase(databaseName: "SupermarketTestDB_Exists")
                .Options;

            var context = new SupermarketContext(options);

            var product = new ProductEntity { Sku = "A", Price = 50 };
            context.Product.Add(product);
            await context.SaveChangesAsync();

            var repo = new ProductPriceRepository(context);

            var result = await repo.GetProductPriceAsync("A");

            Assert.AreEqual(50, result.Price);
        }

        private ProductPriceRepository GetProductPriceRepository()
        {
            return new ProductPriceRepository(
                new SupermarketContext(new DbContextOptions<SupermarketContext>()));
        }

        //TODO: Finish
    }
}
