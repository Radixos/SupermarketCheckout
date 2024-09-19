namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class MultiBuyOfferTests
    {
        [TestMethod]
        public void EnsureSkuQuantityIsMoreThanZeroInCalculateOfferPrice()
        {
            var multiBuyOffer = GetMultiBuyOffer();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                multiBuyOffer.CalculateOfferPrice(0, 50));
        }

        [TestMethod]
        public void EnsureUnitPriceIsNotNegativeInCalculateOfferPrice()
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
