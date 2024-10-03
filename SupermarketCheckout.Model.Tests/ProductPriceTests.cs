namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class ProductPriceTests
    {
        [TestMethod]
        public void ProductPrice_SetsInitialPrice()
        {
            var productPrice = new ProductPrice(50);

            Assert.AreEqual(50, productPrice.Price);
        }

        [TestMethod]
        public void UpdatePrice_UpdatesPrice_WhenNewPriceIsValid()
        {
            var productPrice = new ProductPrice(50);

            productPrice.UpdatePrice(75);

            Assert.AreEqual(75, productPrice.Price);
        }

        [TestMethod]
        public void UpdatePrice_ThrowsArgumentOutOfRangeException_WhenNewPriceIsNegative()
        {
            var productPrice = new ProductPrice(50);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                productPrice.UpdatePrice(-1));
        }

        [TestMethod]
        public void UpdatePrice_ThrowsArgumentException_WhenNewPriceIsSameAsCurrentPrice()
        {
            var productPrice = new ProductPrice(50);

            Assert.ThrowsException<ArgumentException>(() =>
                productPrice.UpdatePrice(50));
        }
    }
}
