using SupermarketCheckout.API.Mappers;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Tests
{
    [TestClass]
    public class ProductPriceMapperTestss
    {
        [TestMethod]
        public void
            MapToProductPriceResponse_ThrowsArgumentNullException_WhenProductPriceDtoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                ProductPriceMapper.MapToProductPriceResponse(null));
        }

        [TestMethod]
        public void MapToProductPriceResponse_CorrectlyMapsProperties()
        {
            var productPriceDto = new ProductPriceDto
            {
                Price = 50
            };

            var productPriceResponse =
                ProductPriceMapper.MapToProductPriceResponse(productPriceDto);

            Assert.AreEqual(productPriceDto.Price, productPriceResponse.Price);
        }
    }
}
