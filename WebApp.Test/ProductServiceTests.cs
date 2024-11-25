using Microsoft.EntityFrameworkCore;
using WebApp.Web.Data;
using WebApp.Web.Domain;
using WebApp.Web.Services;

namespace WebApp.Test
{
    public class ProductServiceTests
    {
        private static WebAppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<WebAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new WebAppDbContext(options);

            // Limpando banco antes de iniciar
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        private static void SeedDatabase(WebAppDbContext context)
        {
            context.Products.AddRange(
                new Product { Name = "Product 1", Sku = "ABC-0001" },
                new Product { Name = "Product 2", Sku = "ABC-0002" }
            );
            context.SaveChanges();
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            SeedDatabase(context);
            var service = new ProductService(context);

            // Act
            var products = await service.GetProductsAsync();

            // Assert
            Assert.NotNull(products);
            Assert.Equal(2, products.Count);
            Assert.Contains(products, p => p.Name == "Product 1");
            Assert.Contains(products, p => p.Name == "Product 2");
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            SeedDatabase(context);
            var service = new ProductService(context);

            // Act
            var product = await service.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(1, product.Id);
            Assert.Equal("Product 1", product.Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            SeedDatabase(context);
            var service = new ProductService(context);

            // Act
            var product = await service.GetProductByIdAsync(999);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var service = new ProductService(context);

            // Act
            var products = await service.GetProductsAsync();

            // Assert
            Assert.NotNull(products);
            Assert.Empty(products);
        }
    }
}