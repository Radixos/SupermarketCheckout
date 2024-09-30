using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestMethod]
        public void
            ProductServiceConstructor_ThrowsArgumentNullException_WhenProductRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ProductService(null, GetOfferRepository()));
        }

        [TestMethod]
        public void
            ProductServiceConstructor_ThrowsArgumentNullException_WhenOfferRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ProductService(GetProductRepository(), null));
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            var service = new ProductService(GetProductRepository(), GetOfferRepository());

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                service.GetProductAsync(null));
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            var service = GetProductService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                service.GetProductAsync(""));
        }

        [TestMethod]
        public async Task
            AddProductAsync_ThrowsArgumentNullException_WhenProductDtoIsNull()
        {
            var service = GetProductService();

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                service.AddProductAsync(null));
        }

        private ProductService GetProductService()
        {
            return new ProductService(GetProductRepository(), GetOfferRepository());
        }

        private ProductRepository GetProductRepository()
        {
            return new ProductRepository(
                new SupermarketContext(new DbContextOptions<SupermarketContext>()));
        }

        private OfferRepository GetOfferRepository()
        {
            return new OfferRepository(
                new SupermarketContext(new DbContextOptions<SupermarketContext>()),
                new OfferFactory());
        }

        //TODO: Finish
    }
}
