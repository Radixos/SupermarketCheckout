using Microsoft.EntityFrameworkCore;
using Moq;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductOfferServiceTests
    {
        [TestMethod]
        public void
            ProductOfferServiceController_ThrowsArgumentNullException_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductOfferService(null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenSkuIsNull_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync(null, "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenSkuIsEmpty_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync("", "MultiBuy"));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenOfferTypeIsNull_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync("A", null));
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsArgumentException_WhenOfferTypeIsEmpty_Async()
        {
            var productOfferService = GetProductOfferService();

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                productOfferService.GetOfferAsync("A", ""));
        }

        [TestMethod]
        public async Task GetOfferAsync_ReturnsOfferDto_WhenOfferExists()
        {
            var offerFactory = GetOfferFactory();
            var offer = offerFactory.CreateOffer("MultiBuy", 3, 130);
            var repositoryMock = new Mock<IOfferRepository>();
            repositoryMock.Setup(r => r.GetOfferAsync("ValidSku", "ValidOfferType"))
                .ReturnsAsync(offer);

            var service = new ProductOfferService(repositoryMock.Object);
            var result = await service.GetOfferAsync("ValidSku", "ValidOfferType");

            Assert.IsNotNull(result);
            Assert.AreEqual(offer.OfferQuantity, result.OfferQuantity);
            Assert.AreEqual(offer.OfferPrice, result.OfferPrice);
        }

        [TestMethod]
        public async Task GetOfferAsync_ThrowsNotFoundException_WhenOfferDoesNotExist()
        {
            var repositoryMock = new Mock<IOfferRepository>();
            repositoryMock
                .Setup(r => r.GetOfferAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((Offer?)null);

            var service = new ProductOfferService(repositoryMock.Object);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                service.GetOfferAsync("ValidSku", "InvalidOfferType"));
        }

        private OfferFactory GetOfferFactory()
        {
            return new OfferFactory();
        }

        private ProductOfferService GetProductOfferService()
        {
            return new ProductOfferService(new OfferRepository(
                new SupermarketContext(new DbContextOptions<SupermarketContext>()),
                new OfferFactory()));
        }
    }
}
