namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class MultiBuyOfferTests
    {
        [TestMethod]
        public void CalculateOfferPrice_ThrowsArgumentOutOfRangeException_WhenSkuQuantityIsZeroOrLess()
        {
            var multiBuyOffer = GetMultiBuyOffer();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                multiBuyOffer.CalculateOfferPrice(0, 50));
        }

        [TestMethod]
        public void CalculateOfferPrice_ThrowsArgumentOutOfRangeException_WhenUnitPriceIsLessThanZero()
        {
            var multiBuyOffer = GetMultiBuyOffer();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                multiBuyOffer.CalculateOfferPrice(1, -1));
        }

        [TestMethod]
        public void CalculateOfferPrice_ReturnsCorrectPrice_WhenValidSkuQuantityAndUnitPriceAreProvided()
        {
            var multiBuyOffer = GetMultiBuyOffer();
            var result = multiBuyOffer.CalculateOfferPrice(5, 50); // 5 items with unit price of 50
            Assert.AreEqual(230, result); // (3 for 130) + (2 for 50 each) = 230
        }

        private MultiBuyOffer GetMultiBuyOffer()
        {
            return new MultiBuyOffer(3, 130);
        }
    }
}
