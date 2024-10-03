using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Model.Exceptions;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef.Tests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private DbContextOptions<SupermarketContext> _options;

        [TestInitialize]
        public void SetUp()
        {
            var dbName = Guid.NewGuid().ToString();
            _options = new DbContextOptionsBuilder<SupermarketContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [TestMethod]
        public void ProductRepositoryConstructor_ThrowsArgumentNullException_WhenContextIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProductRepository(null));
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ThrowsNotFoundException_WhenNoProductsExist()
        {
            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductRepository(context);
                await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                    repo.GetAllProductsAsync());
            }
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ReturnsProducts_WhenProductsExist()
        {
            using (var context = new SupermarketContext(_options)) {
                context.Product.Add(new ProductEntity
                {
                    Sku = "A",
                    Price = 50
                });
                context.Product.Add(new ProductEntity
                {
                    Sku = "B",
                    Price = 30
                });
                await context.SaveChangesAsync();

                var repo = new ProductRepository(context);
                var products = await repo.GetAllProductsAsync();

                Assert.IsNotNull(products);
                Assert.AreEqual(2, products.Count);
                Assert.AreEqual("A", products[0].Sku);
                Assert.AreEqual(50, products[0].Price);
                Assert.AreEqual("B", products[1].Sku);
                Assert.AreEqual(30, products[1].Price);
            }
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsArgumentException_WhenSkuIsNull()
        {
            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductRepository(context);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                    repo.GetProductAsync(null));
            }
        }

        [TestMethod]
        public async Task GetProductAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductRepository(context);
                await Assert.ThrowsExceptionAsync<NotFoundException>(() =>
                    repo.GetProductAsync("NonExistingSku"));
            }
        }

        [TestMethod]
        public async Task GetProductAsync_ReturnsCorrectProduct_WhenProductExists()
        {
            using (var context = new SupermarketContext(_options)) {
                var productEntity = new ProductEntity
                {
                    Sku = "A",
                    Price = 50
                };
                context.Product.Add(productEntity);
                await context.SaveChangesAsync();

                var repo = new ProductRepository(context);
                var product = await repo.GetProductAsync("A");

                Assert.IsNotNull(product);
                Assert.AreEqual("A", product.Sku);
                Assert.AreEqual(50, product.Price);
            }
        }

        [TestMethod]
        public async Task AddProductAsync_ThrowsArgumentNullException_WhenProductIsNull()
        {
            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductRepository(context);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => repo.AddProductAsync(null));
            }
        }

        [TestMethod]
        public async Task AddProductAsync_ThrowsInvalidOperationException_WhenProductAlreadyExists()
        {
            using (var context = new SupermarketContext(_options)) {
                var productEntity = new ProductEntity
                {
                    Sku = "A",
                    Price = 50
                };
                context.Product.Add(productEntity);
                await context.SaveChangesAsync();

                var repo = new ProductRepository(context);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                    repo.AddProductAsync(new Product("A", 60, null)));
            }
        }

        [TestMethod]
        public async Task AddProductAsync_AddsProduct_WhenProductIsNew()
        {
            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductRepository(context);
                var newProduct = new Product("B", 30, null);
                await repo.AddProductAsync(newProduct);

                var addedProduct = await context.Product.FirstOrDefaultAsync(p => p.Sku == "B");
                Assert.IsNotNull(addedProduct);
                Assert.AreEqual("B", addedProduct.Sku);
                Assert.AreEqual(30, addedProduct.Price);
            }
        }

        [TestMethod]
        public async Task DeleteProductAsync_ThrowsArgumentNullException_WhenProductIsNull()
        {
            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductRepository(context);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => repo.DeleteProductAsync(null));
            }
        }

        [TestMethod]
        public async Task DeleteProductAsync_ThrowsNotFoundException_WhenProductDoesNotExist()
        {
            using (var context = new SupermarketContext(_options)) {
                var repo = new ProductRepository(context);
                var product = new Product("NonExistingSku", 50, null);
                await Assert.ThrowsExceptionAsync<NotFoundException>(() => repo.DeleteProductAsync(product));
            }
        }

        [TestMethod]
        public async Task DeleteProductAsync_DeletesProduct_WhenProductExists()
        {
            using (var context = new SupermarketContext(_options)) {
                var productEntity = new ProductEntity
                {
                    Sku = "A",
                    Price = 50
                };
                context.Product.Add(productEntity);
                await context.SaveChangesAsync();

                var repo = new ProductRepository(context);
                var productToDelete = new Product("A", 50, null);
                await repo.DeleteProductAsync(productToDelete);

                var deletedProduct = await context.Product.FirstOrDefaultAsync(p => p.Sku == "A");
                Assert.IsNull(deletedProduct);
            }
        }
    }
}
