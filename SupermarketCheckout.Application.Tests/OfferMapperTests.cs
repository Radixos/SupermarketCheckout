using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class OfferMapperTests
    {
        [TestMethod]
        public void MapToOfferDto_ReturnsNull_WhenProductOfferIsNull()
        {
            Offer offer = null;

            var offerDto = OfferMapper.MapToOfferDto(offer);

            Assert.IsNull(offerDto);
        }

        [TestMethod]
        public void MapToOfferDto_CorrectlyMapsProperties()
        {
            var offerFactory = new OfferFactory();
            var offer = offerFactory.CreateOffer("MultiBuy", 3, 130);

            var offerDto = OfferMapper.MapToOfferDto(offer);

            Assert.AreEqual(offer.OfferQuantity, offerDto?.Quantity);
            Assert.AreEqual(offer.OfferPrice, offerDto?.Price);
        }
    }
}
