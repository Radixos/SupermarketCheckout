namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        public void BasketController_ThrowsArgumentNullException_WhenSkusIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Basket(null));
        }

        [TestMethod]
        public void BasketController_ThrowsArgumentNullException_WhenSkusIsEmpty()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new Basket(new List<string>()));
        }

        [TestMethod]
        public async Task GetTotalPriceAsync_ThrowsArgumentNullException_WhenRepositoryIsNotSupplied_Async()
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