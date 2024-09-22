namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        public void EnsureSKUsIsNotNullInTheConstructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Basket(null));
        }

        [TestMethod]
        public void EnsureSKUsIsNotEmptyInTheConstructor()
        {
            Assert.ThrowsException<ArgumentException>(() => new Basket(new List<string>()));
        }

        [TestMethod]
        public async Task EnsureItemCatalogRepositoryIsSuppiedToGetTotalPriceAsync()
        {
            var basket = GetBasket();
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                basket.GetTotalPriceAsync(null));
        }

        private Basket GetBasket()
        {
            return new Basket(new List<string> { "A", "B", "C", "D" });
        }
    }
}