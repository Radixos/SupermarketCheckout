using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Repositories.Ef;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductPriceServiceTests
    {
        [TestMethod]
        public void
            ProductPriceServiceController_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductPriceService(null));
        }

        [TestMethod]
        public async Task GetProductPriceAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                new ProductPriceService(new ProductPriceRepository(
                        new SupermarketContext(
                            new DbContextOptions<SupermarketContext>())))
                    .GetProductPriceAsync(null));
        }

        //TODO: Finish
    }
}
