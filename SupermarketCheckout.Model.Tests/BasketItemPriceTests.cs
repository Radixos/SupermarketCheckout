namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class BasketItemPriceTests
    {
        [TestMethod]
        public void EnsureUnitPriceIsNotNullInTheConstructor()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BasketItemPrice(decimal.MinusOne, null));
        }

        [TestMethod]
        public void EnsureHasOfferReturnsFalseIfNoOffer()
        {
            var basketItemPrice = new BasketItemPrice(50, null);
            Assert.AreEqual(false, basketItemPrice.HasOffer);
        }

        [TestMethod]
        public void EnsureHasOfferCanReturnsTrueIfOffer()
        {
            var basketItemPrice = GetBasketItemPriceWithOffer();
            Assert.AreEqual(true, basketItemPrice.HasOffer);
        }

        [TestMethod]
        public void EnsureSkuQuantityIsProvidedToCalculateTotalBasketItemPrice()
        {
            var basketItemPrice = GetBasketItemPriceWithOffer();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                basketItemPrice.CalculateTotalBasketItemPrice(0));
        }

        [TestMethod]
        public void EnsureCalculateTotalBasketItemPriceDoesntReturnOfferPriceIfHasOfferIsTrue()
        {
            var basketItemPrice = new BasketItemPrice(50, null);
            Assert.AreEqual(100, basketItemPrice.CalculateTotalBasketItemPrice(2)); //2*50
        }

        [TestMethod]
        public void EnsureCalculateTotalBasketItemPriceReturnsOfferPriceIfHasOfferIsFalse()
        {
            var basketItemPrice = GetBasketItemPriceWithOffer();
            Assert.AreEqual(130, basketItemPrice.CalculateTotalBasketItemPrice(3)); //130
        }

        private BasketItemPrice GetBasketItemPriceWithOffer()
        {
            return new BasketItemPrice(
                50,
                new MultiBuyOffer(3, 130));
        }
    }
}
