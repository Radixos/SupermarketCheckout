﻿using Moq;
using SupermarketCheckout.Model.Repositories;

namespace SupermarketCheckout.Model.Tests
{
    [TestClass]
    public class BasketItemTests
    {
        [TestMethod]
        public void EnsureSkusIsNotEmptyInTheConstructor()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new BasketItem("", 1));
        }

        [TestMethod]
        public void EnsureQuantityIsNotZeroInTheConstructor()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BasketItem("A", 0));
        }

        [TestMethod]
        public async Task EnsureItemCatalogRepositoryIsProvidedToGetTotalPriceAsync()
        {
            var basket = GetBasketItem();

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
                await basket.GetTotalPriceAsync(null));
        }

        [TestMethod]
        public async Task EnsureRepoIsCalledOnceForEachBasketItemAsync()
        {
            var basketItem = GetBasketItem();

            var repoMock = new Mock<IItemCatalogRepository>();
            repoMock
                .Setup(m =>
                    m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()))
                .ReturnsAsync(new BasketItemPrice(50, null));

            await basketItem.GetTotalPriceAsync(repoMock.Object);

            repoMock.Verify(m =>
                m.GetBasketItemPriceBySkuAsync(It.IsAny<string>()), Times.Once);
        }

        private BasketItem GetBasketItem()
        {
            return new BasketItem("A", 1);
        }
    }
}
