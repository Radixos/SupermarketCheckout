namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class OfferTests
    {
        [TestMethod]
        public void EnsureOfferTypeIsSuppliedToCreateOffer()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentException>(() =>
                offerFactory.CreateOffer(null, 3, 130));
        }

        [TestMethod]
        public void EnsureOfferQuantityIsValidInCreateOffer()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                offerFactory.CreateOffer("MultiBuy", 0, 130));
        }

        [TestMethod]
        public void EnsureOfferPriceIsValidInCreateOffer()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                offerFactory.CreateOffer("MultiBuy", 3, -1));
        }

        [TestMethod]
        public void EnsureCreateOfferThrowsForUnrecognisedOfferType()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                offerFactory.CreateOffer("NotExistingOffer", 3, 130));
        }
    }
}
