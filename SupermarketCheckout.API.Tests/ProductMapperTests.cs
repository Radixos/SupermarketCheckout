using SupermarketCheckout.API.DTOs;
using SupermarketCheckout.API.Mappers;
using SupermarketCheckout.Application.DTOs;

namespace SupermarketCheckout.API.Tests
{
    [TestClass]
    public class ProductMapperTests
    {
        [TestMethod]
        public void MapToProduct_ThrowsArgumentNullException_WhenProductDtoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                ProductMapper.MapToProduct(null));
        }

        [TestMethod]
        public void MapToProduct_CorrectlyMapsProperties()
        {
            var productDto = new ProductDto
            {
                Sku = "A",
                Price = 50,
                Offer = null
            };

            var product = ProductMapper.MapToProduct(productDto);

            Assert.AreEqual(productDto.Sku, product.Sku);
            Assert.AreEqual(productDto.Price, product.Price);
            Assert.IsNull(product.Offer);
        }

        [TestMethod]
        public void MapToProductsResponse_ThrowsArgumentNullException_WhenProductDtoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                ProductMapper.MapToProductsResponse(null));
        }

        [TestMethod]
        public void MapToProductsResponse_CorrectlyMapsProperties()
        {
            var products = new List<ProductDto>
            {
                new ProductDto
                {
                    Sku = "A",
                    Price = 50,
                    Offer = null
                },
                new ProductDto
                {
                    Sku = "B",
                    Price = 30,
                    Offer = null
                }
            };

            var productsResponse = ProductMapper.MapToProductsResponse(products);

            Assert.AreEqual(products[0].Sku, productsResponse.Products[0].Sku);
            Assert.AreEqual(products[0].Price, productsResponse.Products[0].Price);
            Assert.IsNull(productsResponse.Products[0].Offer);
            Assert.AreEqual(products[1].Sku, productsResponse.Products[1].Sku);
            Assert.AreEqual(products[1].Price, productsResponse.Products[1].Price);
            Assert.IsNull(productsResponse.Products[1].Offer);
        }

        [TestMethod]
        public void MapToProductDto_ThrowsArgumentNullException_WhenProductDtoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                ProductMapper.MapToProductDto(null));
        }

        [TestMethod]
        public void MapToProductDto_CorrectlyMapsProperties()
        {
            var product = new Product
            {
                Sku = "A",
                Price = 50,
                Offer = null
            };

            var productDto = ProductMapper.MapToProductDto(product);

            Assert.AreEqual(product.Sku, productDto.Sku);
            Assert.AreEqual(product.Price, productDto.Price);
            Assert.IsNull(product.Offer);
        }
    }
}
