using Moq;
using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;
using SupermarketCheckout.Repositories.Ef.Mappers;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class BasketItemMapperTests
    {
        private Mock<IOfferFactory> _offerFactoryMock;

        [TestInitialize]
        public void Setup()
        {
            _offerFactoryMock = new Mock<IOfferFactory>();
        }

        [TestMethod]
        public void MapToBasketItemPrice_ReturnsNull_WhenEntityIsNull()
        {
            var result = BasketItemMapper.MapToBasketItemPrice(null, _offerFactoryMock.Object);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToBasketItemPrice_ReturnsBasketItemPrice_WhenEntityIsValid_AndOfferIsNull()
        {
            var sku = "A";
            var productEntity = new ProductEntity {
                Sku = sku,
                Price = 50,
                OfferId = null
            };

            var result = BasketItemMapper.MapToBasketItemPrice(productEntity, _offerFactoryMock.Object);

            Assert.IsNotNull(result);
            Assert.AreEqual(50, result.UnitPrice);
            Assert.IsNull(result.Offer);
        }

        [TestMethod]
        public void MapToBasketItemPrice_ReturnsBasketItemPrice_WhenEntityIsValid_AndOfferExists()
        {
            var sku = "A";
            var productEntity = new ProductEntity
            {
                Sku = sku,
                Price = 50,
                OfferId = 1,
                Offer = new OfferEntity
                {
                    OfferId = 1,
                    OfferType = "MultiBuy",
                    OfferQuantity = 2,
                    OfferPrice = 10,
                    BasketItems = new List<ProductEntity>()
                }
            };

            var multiBuyOffer = new MultiBuyOffer(productEntity.Offer.OfferQuantity.Value,
                productEntity.Offer.OfferPrice.Value);

            _offerFactoryMock
                .Setup(offerFactory => offerFactory.CreateOffer(
                    productEntity.Offer.OfferType,
                    productEntity.Offer.OfferQuantity.Value,
                    productEntity.Offer.OfferPrice.Value))
                .Returns(multiBuyOffer);

            var result =
                BasketItemMapper.MapToBasketItemPrice(productEntity,
                    _offerFactoryMock.Object);

            Assert.IsNotNull(result, "The result should not be null");
            Assert.AreEqual(50, result.UnitPrice, "The UnitPrice should match the product price");
            Assert.IsNotNull(result.Offer, "The offer should not be null");
            Assert.AreEqual(multiBuyOffer.OfferQuantity, result.Offer.OfferQuantity, "The offer quantity should match");
            Assert.AreEqual(multiBuyOffer.OfferPrice, result.Offer.OfferPrice, "The offer price should match");
        }

        [TestMethod]
        public void MapToBasketItemPrice_ReturnsBasketItemPriceWithoutOffer_WhenOfferIsNull()
        {
            var productEntity = new ProductEntity
            {
                Sku = "A",
                Price = 50,
                OfferId = null,
                Offer = null
            };

            var result = BasketItemMapper.MapToBasketItemPrice(productEntity, _offerFactoryMock.Object);

            Assert.IsNotNull(result);
            Assert.AreEqual(50, result.UnitPrice);
            Assert.IsNull(result.Offer);
        }

        [TestMethod]
        public void MapToOffer_ReturnsNull_WhenEntityIsNull()
        {
            var result = BasketItemMapper.MapToOffer(null, _offerFactoryMock.Object);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToOffer_ReturnsNull_WhenOfferQuantityOrPriceIsNull()
        {
            var offerEntity = new OfferEntity
            {
                OfferType = "MultiBuy",
                OfferQuantity = null,
                OfferPrice = 10,
                BasketItems = new List<ProductEntity>()
            };

            var result = BasketItemMapper.MapToOffer(offerEntity, _offerFactoryMock.Object);

            Assert.IsNull(result);

            offerEntity.OfferQuantity = 2;
            offerEntity.OfferPrice = null;

            result = BasketItemMapper.MapToOffer(offerEntity, _offerFactoryMock.Object);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MapToOffer_ReturnsOffer_WhenEntityIsValid()
        {
            var offerEntity = new OfferEntity
            {
                OfferType = "MultiBuy",
                OfferQuantity = 2,
                OfferPrice = 10,
                BasketItems = new List<ProductEntity>()
            };

            var expectedOffer = new MultiBuyOffer(offerEntity.OfferQuantity.Value, offerEntity.OfferPrice.Value);

            _offerFactoryMock
                .Setup(offerFactory => offerFactory.CreateOffer(
                    offerEntity.OfferType,
                    offerEntity.OfferQuantity.Value,
                    offerEntity.OfferPrice.Value))
                .Returns(expectedOffer);

            var result = BasketItemMapper.MapToOffer(offerEntity, _offerFactoryMock.Object);

            Assert.IsNotNull(result, "The result should not be null");
            Assert.AreEqual(expectedOffer.OfferQuantity, result.OfferQuantity, "The offer quantity should match");
            Assert.AreEqual(expectedOffer.OfferPrice, result.OfferPrice, "The offer price should match");
        }
    }
}
