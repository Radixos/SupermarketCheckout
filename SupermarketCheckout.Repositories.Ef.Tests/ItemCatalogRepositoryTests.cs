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
            Assert.ThrowsException<ArgumentNullException>(() => new ItemCatalogRepository(null));
        }

        [TestMethod]
        public async Task EnsureSKUIsNotEmptyInGetBasketItemPriceBySKUAsync()
        {
            var repo = GetItemCatalogRepository();

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() =>
                repo.GetBasketItemPriceBySKUAsync(" "));
        }

        private ItemCatalogRepository GetItemCatalogRepository()
        {
            return new ItemCatalogRepository(GetSupermarketContext());
        }

        private SupermarketContext GetSupermarketContext()
        {
            return new SupermarketContext(new DbContextOptions<SupermarketContext>());
        }
    }
}