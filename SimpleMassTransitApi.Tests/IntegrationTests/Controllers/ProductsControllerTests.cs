using Core.Contracts.Controllers.Products;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace SimpleMassTransitApi.Tests.IntegrationTests.Controllers
{
    public sealed class ProductsControllerTests
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsControllerTests()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(b =>
                {
                    b.ConfigureServices((services) =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<ApiDataContext>));

                        if (descriptor != null)
                            services.Remove(descriptor);

                        services.AddDbContext<ApiDataContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryProductsController");
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_WithExistingProducts_ReturnsOkResultWithProducts()
        {
            // Arrange
            Product[] expectedProducts =
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "First",
                    Price = 1,
                    CategoryId = Guid.NewGuid(),
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Second",
                    Price = 2,
                    CategoryId = Guid.NewGuid(),
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Third",
                    Price = 3,
                    CategoryId = Guid.NewGuid(),
                },
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.Products.AddRange(expectedProducts);
                dbContext.SaveChanges();
            }


            // Act
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var products = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<Product[]>(responseContent)!;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            products.Should().BeEquivalentTo(expectedProducts);
        }


        #region GetProduct

        [Fact]
        public async Task GetProduct_WhenExist_ReturnOkProduct()
        {
            //Arrange
            Product product = new()
            {
                ProductId = Guid.NewGuid(),
                Name = "First",
                Price = 1,
                CategoryId = Guid.NewGuid(),
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }

            //Act
            var response = await _client.GetAsync($"/api/products/{product.ProductId}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<Product>(responseContent)!;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task GetProduct_WhenNotExist_ReturnNotFound()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.SaveChanges();
            }

            //Act
            var response = await _client.GetAsync($"/api/products/{Guid.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion


        [Fact]
        public async Task CreateProduct_ReturnOk()
        {
            // Arrange
            Category category = new()
            {
                CategoryId = Guid.NewGuid(),
                Name = "Test"
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
            }

            var createProductRequest = new CreateProductRequest("NewProduct", 1, category.CategoryId);
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(createProductRequest),
                System.Text.Encoding.UTF8,
                "application/json");

            // Act
            var postResponse = await _client.PostAsync("/api/products", content);

            var getResponse = await _client.GetAsync("/api/products");
            getResponse.EnsureSuccessStatusCode();
            var responseContent = await getResponse.Content.ReadAsStringAsync();

            var products = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<Product[]>(responseContent)!;

            // Assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            products.Should().Contain(c => c.Name == createProductRequest.Name);
        }


        #region UpdateProduct

        [Fact]
        public async Task UpdateProduct_WhenExist_ReturnOk()
        {
            //Arrange
            Product product = new()
            {
                ProductId = Guid.NewGuid(),
                Name = "First",
                Price = 1,
                CategoryId = Guid.NewGuid(),
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }

            var updateProductRequest = new UpdateProductRequest(product.ProductId, "NewName", product.Price, product.CategoryId);
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(updateProductRequest),
                System.Text.Encoding.UTF8,
                "application/json");


            //Act
            var putResponse = await _client.PutAsync("/api/products", content);

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateProduct_WhenNotExist_ReturnNotFound()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.SaveChanges();
            }

            var updateProductRequest = new UpdateProductRequest(Guid.NewGuid(), "NewName", 1, Guid.NewGuid());
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(updateProductRequest),
                System.Text.Encoding.UTF8,
                "application/json");


            //Act
            var putResponse = await _client.PutAsync("/api/products", content);

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion


        #region DeleteProduct

        [Fact]
        public async Task DeleteProduct_WhenExist_ReturnOk()
        {
            //Arrange
            Product product = new()
            {
                ProductId = Guid.NewGuid(),
                Name = "First",
                Price = 1,
                CategoryId = Guid.NewGuid(),
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }

            //Act
            var putResponse = await _client.DeleteAsync($"/api/products/{product.ProductId}");

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteProduct_WhenNotExist_ReturnNotFound()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.SaveChanges();
            }

            //Act
            var putResponse = await _client.DeleteAsync($"/api/products/{Guid.NewGuid()}");

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion
    }
}
