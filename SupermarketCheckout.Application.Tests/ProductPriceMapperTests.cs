using SupermarketCheckout.Application.DTOs;
using SupermarketCheckout.Application.Mappers;
using SupermarketCheckout.Model;

namespace SupermarketCheckout.Application.Tests
{
    [TestClass]
    public class ProductPriceMapperTests
    {
        [TestMethod]
        public void MapToProductPriceDto_ThrowsArgumentNullException_WhenProductIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                ProductPriceMapper.MapToProductPriceDto(null));
        }

        [TestMethod]
        public void MapToProductPriceDto_CorrectlyMapsProperties()
        {
            var productPrice = new ProductPrice
            {
                Price = 50
            };

            var productPriceDto = ProductPriceMapper.MapToProductPriceDto(productPrice);

            Assert.AreEqual(productPrice.Price, productPriceDto.Price);
        }
    }
}
