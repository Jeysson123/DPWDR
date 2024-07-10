using NUnit.Framework;
using System.Threading.Tasks;
using Technical.Interview.Model;
using Technical.Interview.Model.Entities;
using Technical.Interview.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace Technical.Interview.Tester
{
    /*class for unit test, to secure all core functionalities*/

    public class ProductServiceTests
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private ProductService _productService;
        private Mock<ILogger<ProductService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ProductService>>();

            _productService = new ProductService(_context, _loggerMock.Object);
        }

        [Test]
        public void AddProducts()
        {
            var products = new[]
            {
                new Product { Title = "Product 1", Price = 10.99, Description = "Description 1", Category = "Category 1" },
                new Product { Title = "Product 2", Price = 19.99, Description = "Description 2", Category = "Category 2" },
                new Product { Title = "Product 3", Price = 5.99, Description = "Description 3", Category = "Category 1" },
                new Product { Title = "Product 1", Price = 10.99, Description = "Description 1", Category = "Category 1" },
                new Product { Title = "Product 2", Price = 19.99, Description = "Description 2", Category = "Category 2" },
                new Product { Title = "Product 3", Price = 5.99, Description = "Description 3", Category = "Category 1" },
                new Product { Title = "Product 1", Price = 10.99, Description = "Description 1", Category = "Category 1" },
                new Product { Title = "Product 2", Price = 19.99, Description = "Description 2", Category = "Category 2" },
                new Product { Title = "Product 3", Price = 5.99, Description = "Description 3", Category = "Category 1" }
            };

            _context.Product.AddRange(products);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllProducts()
        {
            var products = await _productService.GetAllProducts();

            Assert.IsNotNull(products);
        }

        [Test]
        public async Task GetUniqueProduct()
        {
            int existingProductId = 1;

            var product = await _productService.GetUniqueProduct(existingProductId);

            Assert.IsNotNull(product);
            Assert.AreEqual(existingProductId, product.Id);
        }

        [Test]
        public async Task UpdateProduct()
        {
            int existingProductId = 2;

            var existingProduct = await _productService.GetUniqueProduct(existingProductId);

            existingProduct.Price = 25.99;

            var result = await _productService.UpdateProduct(existingProduct);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteProduct()
        {
            int existingProductId = 3;

            var result = await _productService.DeleteProduct(existingProductId);

            Assert.IsTrue(result);
        }
    }
}
