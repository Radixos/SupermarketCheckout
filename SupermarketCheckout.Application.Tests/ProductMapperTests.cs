using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductMapperTests
    {
        [TestMethod]
        public void MapToProductDto_ThrowsArgumentNullException_WhenProductIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                ProductMapper.MapToProductDto(null));
        }

        [TestMethod]
        public void MapToProductDto_CorrectlyMapsProperties()
        {
            var product = new Product("A", 50, "MultiBuy");

            var productDto = ProductMapper.MapToProductDto(product);

            Assert.AreEqual(product.Sku, productDto.Sku);
            Assert.AreEqual(product.Price, productDto.Price);
            Assert.AreEqual(product.OfferType, productDto.OfferType);
        }
    }
}
