using SupermarketCheckout.Repositories.Ef.Mappers;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class ProductPriceMapperTests
    {
        [TestMethod]
        public void
            MapToProductPrice_ThrowsArgumentOutOfRangeException_WhenPriceIsNegative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                ProductPriceMapper.MapToProductPrice(-1));
        }

        [TestMethod]
        public void MapToProductPrice_ReturnsProductPrice_WhenPriceIsValid()
        {
            decimal price = 10;

            var result = ProductPriceMapper.MapToProductPrice(price);

            Assert.IsNotNull(result);
            Assert.AreEqual(price, result.Price);
        }
    }
}
