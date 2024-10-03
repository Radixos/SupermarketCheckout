using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Mappers;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Tests
{
    [TestClass]
    public class OfferMapperTests
    {
        [TestMethod]
        public void MapToOfferResponse_ReturnsNull_WhenOfferDtoIsNull()
        {
            OfferDto offerDto = null;

            var offer = OfferMapper.MapToOfferResponse(offerDto);

            Assert.IsNull(offer);
        }

        [TestMethod]
        public void MapToOfferResponse_CorrectlyMapsProperties()
        {
            var offerDto = new OfferDto
            {
                Type = "MultiBuy",
                Quantity = 3,
                Price = 130
            };

            var offerResponse = OfferMapper.MapToOfferResponse(offerDto);

            Assert.AreEqual(offerDto.Quantity, offerResponse?.OfferQuantity);
            Assert.AreEqual(offerDto.Price, offerResponse?.OfferPrice);
        }

        [TestMethod]
        public void MapToOffer_ReturnsNull_WhenOfferDtoIsNull()
        {
            OfferDto offerDto = null;

            var offer = OfferMapper.MapToOffer(offerDto);

            Assert.IsNull(offer);
        }

        [TestMethod]
        public void MapToOffer_CorrectlyMapsProperties()
        {
            var offerDto = new OfferDto {
                Type = "MultiBuy",
                Quantity = 3,
                Price = 130
            };

            var offer = OfferMapper.MapToOffer(offerDto);

            Assert.AreEqual(offerDto.Type, offer?.Type);
            Assert.AreEqual(offerDto.Quantity, offer?.Quantity);
            Assert.AreEqual(offerDto.Price, offer?.Price);
        }

        [TestMethod]
        public void MapToOfferDto_ReturnsNull_WhenOfferIsNull()
        {
            Offer offer = null;

            var offerDto = OfferMapper.MapToOfferDto(offer);

            Assert.IsNull(offerDto);
        }

        [TestMethod]
        public void MapToOfferDto_CorrectlyMapsProperties()
        {
            var offer = new Offer
            {
                Type = "MultiBuy",
                Quantity = 3,
                Price = 130
            };

            var offerDto = OfferMapper.MapToOfferDto(offer);

            Assert.AreEqual(offer.Type, offerDto?.Type);
            Assert.AreEqual(offer.Quantity, offerDto?.Quantity);
            Assert.AreEqual(offer.Price, offerDto?.Price);
        }
    }
}
