using Microsoft.EntityFrameworkCore;
using Moq;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class ItemCatalogRepositoryTests
    {
        [TestMethod]
        public void EnsureSupermarketContextIsProvidedToConstructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ItemCatalogRepository(null, new OfferFactory()));
        }

        [TestMethod]
        public async Task EnsureSkuIsNotEmptyInGetBasketItemPriceBySKUAsync()
        {
            var repo = GetItemCatalogRepository();

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
                repo.GetBasketItemPriceBySkuAsync(" "));
        }

        private ItemCatalogRepository GetItemCatalogRepository()
        {
            return new ItemCatalogRepository(GetSupermarketContext(), GetOfferFactory());
        }

        private SupermarketContext GetSupermarketContext()
        {
            return new SupermarketContext(new DbContextOptions<SupermarketContext>());
        }

        private IOfferFactory GetOfferFactory()
        {
            return new OfferFactory();
        }
    }
}