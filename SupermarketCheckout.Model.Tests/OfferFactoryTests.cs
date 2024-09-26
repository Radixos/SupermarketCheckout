namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class OfferFactoryTests
    {
        [TestMethod]
        public void CreateOffer_ThrowsArgumentException_WhenOfferTypeIsNull()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentException>(() =>
                offerFactory.CreateOffer(null, 3, 130));
        }

        [TestMethod]
        public void CreateOffer_ThrowsArgumentException_WhenOfferTypeIsEmpty()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentException>(() =>
                offerFactory.CreateOffer("", 3, 130));
        }

        [TestMethod]
        public void CreateOffer_ThrowsArgumentOutOfRangeException_WhenOfferTypeDoesNotExist()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                offerFactory.CreateOffer("NonExistingOffer", 3, 130));
        }

        [TestMethod]
        public void CreateOffer_ThrowsArgumentOutOfRangeException_WhenOfferQuantityIsZero()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                offerFactory.CreateOffer("MultiBuy", 0, 130));
        }

        [TestMethod]
        public void CreateOffer_ThrowsArgumentOutOfRangeException_WhenOfferPriceIsNegative()
        {
            var offerFactory = new OfferFactory();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                offerFactory.CreateOffer("MultiBuy", 3, -1));
        }
    }
}
