namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class OfferTests
    {
        [TestMethod]
        public void EnsureOfferTypeIsSuppliedToCreateOffer()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                Offer.CreateOffer(null, 3, 130));
        }

        [TestMethod]
        public void EnsureOfferQuantityIsValidInCreateOffer()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                Offer.CreateOffer("MultiBuy", 0, 130));
        }

        [TestMethod]
        public void EnsureOfferPriceIsValidInCreateOffer()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                Offer.CreateOffer("MultiBuy", 3, -1));
        }

        [TestMethod]
        public void EnsureCreateOfferThrowsForUnrecognisedOfferType()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                Offer.CreateOffer("NotExistingOffer", 3, 130));
        }
    }
}
