using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;
using SupermarketCheckout.Repositories.Ef.Mappers;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class ProductMapperTests
    {
        [TestMethod]
        public void MapToProduct_ReturnsProduct_WhenEntityIsValid()
        {
            var productEntity = new ProductEntity {
                Sku = "A",
                Price = 10,
                OfferId = null,
                Offer = null
            };

            var result = ProductMapper.MapToProduct(productEntity);

            Assert.IsNotNull(result);
            Assert.AreEqual(productEntity.Sku, result.Sku);
            Assert.AreEqual(productEntity.Price, result.Price);
            Assert.IsNull(result.Offer);
        }

        [TestMethod]
        public void MapToProduct_ThrowsArgumentNullException_WhenEntityIsNull()
        {
            ProductEntity productEntity = null;

            Assert.ThrowsException<ArgumentNullException>(() => ProductMapper.MapToProduct(productEntity));
        }

        [TestMethod]
        public void MapToProduct_ReturnsProductWithNullOfferType_WhenOfferIsNull()
        {
            var productEntity = new ProductEntity {
                Sku = "A",
                Price = 10,
                OfferId = null,
                Offer = null
            };

            var result = ProductMapper.MapToProduct(productEntity);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Offer);
        }
    }
}
