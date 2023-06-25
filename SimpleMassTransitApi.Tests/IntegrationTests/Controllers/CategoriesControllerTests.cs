using Core.Contracts.Controllers.Categories;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;


namespace SimpleMassTransitApi.Tests.IntegrationTests.Controllers
{
    public sealed class CategoriesControllerTests
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public CategoriesControllerTests()
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
                            options.UseInMemoryDatabase("InMemoryCategoriesController");
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetCategories_WithExistingCategories_ReturnsOkResultWithCategories()
        {
            // Arrange
            Category[] expectedCategories =
            {
                new()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Watersports"
                },
                new()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Football"
                },
                new()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Chess"
                },
            };
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Categories.RemoveRange(dbContext.Categories);
                dbContext.Categories.AddRange(expectedCategories);
                dbContext.SaveChanges();
            }


            // Act
            var response = await _client.GetAsync("/api/categories");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            //var categories = JsonSerializer.Deserialize<List<Category>>(responseContent, new JsonSerializerOptions()
            //{
            //    PropertyNameCaseInsensitive = true
            //})!;

            var categories = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<Category[]>(responseContent)!;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            categories.Should().BeEquivalentTo(expectedCategories);
        }


        #region GetCategory

        [Fact]
        public async Task GetCategory_WhenExist_ReturnOkCategory()
        {
            //Arrange
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

            //Act
            var response = await _client.GetAsync($"/api/categories/{category.CategoryId}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<Category>(responseContent)!;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task GetCategory_WhenNotExist_ReturnNotFound()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.SaveChanges();
            }

            //Act
            var response = await _client.GetAsync($"/api/categories/{Guid.Empty}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion


        [Fact]
        public async Task CreateCategory_ReturnOk()
        {
            // Arrange
            var createCategoryRequest = new CreateCategoryRequest("Test Category");
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(createCategoryRequest),
                System.Text.Encoding.UTF8,
                "application/json");

            // Act
            var postResponse = await _client.PostAsync("/api/categories", content);

            var getResponse = await _client.GetAsync("/api/categories");
            getResponse.EnsureSuccessStatusCode();
            var responseContent = await getResponse.Content.ReadAsStringAsync();

            var categories = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<Category[]>(responseContent)!;

            // Assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            categories.Should().Contain(c => c.Name == createCategoryRequest.Name);
        }


        #region UpdateCategory

        [Fact]
        public async Task UpdateCategory_WhenExist_ReturnOk()
        {
            //Arrange
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

            var updateCategoryRequest = new UpdateCategoryRequest(category.CategoryId, "NewName");
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(updateCategoryRequest),
                System.Text.Encoding.UTF8,
                "application/json");


            //Act
            var putResponse = await _client.PutAsync("/api/categories", content);

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateCategory_WhenNotExist_ReturnNotFound()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Categories.RemoveRange(dbContext.Categories);
                dbContext.SaveChanges();
            }

            var updateCategoryRequest = new UpdateCategoryRequest(Guid.NewGuid(), "NewName");
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(updateCategoryRequest),
                System.Text.Encoding.UTF8,
                "application/json");


            //Act
            var putResponse = await _client.PutAsync("/api/categories", content);

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion


        #region DeleteCategory

        [Fact]
        public async Task DeleteCategory_WhenExist_ReturnOk()
        {
            //Arrange
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

            //Act
            var putResponse = await _client.DeleteAsync($"/api/categories/{category.CategoryId}");

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCategory_WhenNotExist_ReturnNotFound()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApiDataContext>();
                dbContext.Database.EnsureCreated();
                dbContext.Categories.RemoveRange(dbContext.Categories);
                dbContext.SaveChanges();
            }

            //Act
            var putResponse = await _client.DeleteAsync($"/api/categories/{Guid.NewGuid()}");

            //Assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

    }
}
