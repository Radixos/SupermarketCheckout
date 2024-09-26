using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class OfferMapperTests
    {
        [TestMethod]
        public void MapToOfferDto_ThrowsArgumentNullException_WhenOfferIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                OfferMapper.MapToOfferDto(null));   //TODO ASK about warnings
        }

        [TestMethod]
        public void MapToOfferDto_CorrectlyMapsProperties()
        {
            var offerFactory = new OfferFactory();
            var offer = offerFactory.CreateOffer("MultiBuy", 3, 130);

            var offerDto = OfferMapper.MapToOfferDto(offer);

            Assert.AreEqual(offer.OfferQuantity, offerDto.OfferQuantity);
            Assert.AreEqual(offer.OfferPrice, offerDto.OfferPrice);
        }
    }
}
