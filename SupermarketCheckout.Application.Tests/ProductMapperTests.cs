using SupermarketCheckout.Application.DTOs;
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
        public void MapToProductDto_CorrectlyMapsProperties_WhenOfferIsValid()
        {
            var product = new Product("A", 50, GetValidOffer());

            var productDto = ProductMapper.MapToProductDto(product);

            Assert.AreEqual(product.Sku, productDto.Sku);
            Assert.AreEqual(product.Price, productDto.Price);
            Assert.AreEqual(product.Offer?.OfferType, productDto.Offer?.Type);
            Assert.AreEqual(product.Offer?.OfferQuantity, productDto.Offer?.Quantity);
            Assert.AreEqual(product.Offer?.OfferPrice, productDto.Offer?.Price);
        }

        [TestMethod]
        public void MapToProductDto_CorrectlyMapsProperties_WhenOfferIsNull()
        {
            var product = new Product("A", 50, null);

            var productDto = ProductMapper.MapToProductDto(product);

            Assert.AreEqual(product.Sku, productDto.Sku);
            Assert.AreEqual(product.Price, productDto.Price);
            Assert.AreEqual(product.Offer?.OfferType, productDto.Offer?.Type);
            Assert.AreEqual(product.Offer?.OfferQuantity, productDto.Offer?.Quantity);
            Assert.AreEqual(product.Offer?.OfferPrice, productDto.Offer?.Price);
        }

        [TestMethod]
        public void MapToProductsDto_ThrowsArgumentNullException_WhenProductsIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                ProductMapper.MapToProductsDto(null));
        }

        [TestMethod]
        public void MapToProductsDto_CorrectlyMapsProperties_WhenOfferIsValid()
        {
            var products = new List<Product>
            {
                new Product("A", 50, GetValidOffer()),
                new Product("B", 30, GetValidOffer())
            };

            var productsDto = ProductMapper.MapToProductsDto(products);

            for (int i = 0; i < products.Count; i++)
            {
                Assert.AreEqual(products[i].Sku, productsDto[i].Sku);
                Assert.AreEqual(products[i].Price, productsDto[i].Price);
                Assert.AreEqual(products[i].Offer?.OfferType, productsDto[i].Offer?.Type);
                Assert.AreEqual(products[i].Offer?.OfferQuantity, productsDto[i].Offer?.Quantity);
                Assert.AreEqual(products[i].Offer?.OfferPrice, productsDto[i].Offer?.Price);
            }
        }

        [TestMethod]
        public void MapToProductsDto_CorrectlyMapsProperties_WhenOfferIsNull()
        {
            var products = new List<Product>
            {
                new Product("A", 50, null),
                new Product("B", 30, null)
            };

            var productsDto = ProductMapper.MapToProductsDto(products);

            for (int i = 0; i < products.Count; i++)
            {
                Assert.AreEqual(products[i].Sku, productsDto[i].Sku);
                Assert.AreEqual(products[i].Price, productsDto[i].Price);
                Assert.AreEqual(products[i].Offer?.OfferType, productsDto[i].Offer?.Type);
                Assert.AreEqual(products[i].Offer?.OfferQuantity, productsDto[i].Offer?.Quantity);
                Assert.AreEqual(products[i].Offer?.OfferPrice, productsDto[i].Offer?.Price);
            }
        }

        [TestMethod]
        public void MapToProductsDto_CorrectlyMapsProperties()
        {
            var products = new List<Product>
            {
                new Product("A", 50, GetValidOffer()),
                new Product("B", 30, GetValidOffer())
            };

            var productsDto = ProductMapper.MapToProductsDto(products);

            for (int i = 0; i < products.Count; i++)
            {
                Assert.AreEqual(products[i].Sku, productsDto[i].Sku);
                Assert.AreEqual(products[i].Price, productsDto[i].Price);
                Assert.AreEqual(products[i].Offer.OfferType, productsDto[i].Offer.Type);
                Assert.AreEqual(products[i].Offer.OfferQuantity, productsDto[i].Offer.Quantity);
                Assert.AreEqual(products[i].Offer.OfferPrice, productsDto[i].Offer.Price);
            }
        }

        private Offer GetValidOffer()
        {
            return new OfferFactory().CreateOffer("MultiBuy", 3, 130);
        }
    }
}
