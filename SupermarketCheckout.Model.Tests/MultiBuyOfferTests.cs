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

        private MultiBuyOffer GetMultiBuyOffer()
        {
            return new MultiBuyOffer(3, 130);
        }
    }
}
