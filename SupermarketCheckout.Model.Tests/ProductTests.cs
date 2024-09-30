namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductController_ThrowsArgumentException_WhenSkuIsNull()
        {
            Assert.ThrowsException<ArgumentException>(() => new Product(null, 50, null));
        }

        [TestMethod]
        public void ProductController_ThrowsArgumentException_WhenSkuIsEmpty()
        {
            Assert.ThrowsException<ArgumentException>(() => new Product("", 50, null));
        }

        [TestMethod]
        public void ProductController_ThrowsArgumentOutOfRangeException_WhenPriceIsBelowZero()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Product("A", -1, null));
        }
    }
}
