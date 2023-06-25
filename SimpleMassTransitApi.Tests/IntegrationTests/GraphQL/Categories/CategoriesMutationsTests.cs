using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace SimpleMassTransitApi.Tests.IntegrationTests.GraphQL.Categories
{
    public sealed class CategoriesMutationsTests
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public CategoriesMutationsTests()
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
                            options.UseInMemoryDatabase("InMemoryCategoriesMutations");
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CreateCategory_Should_Return_Categories()
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
                //dbContext.Categories.RemoveRange(dbContext.Categories);
                dbContext.Categories.AddRange(expectedCategories);
                dbContext.SaveChanges();
            }
            var query = @"
                mutation {
                    createCategory(request: {name: ""TTTT""}) {
                      categoryId
                      name
                    }
                }";
            var request = new
            {
                query
            };
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(request),
                System.Text.Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/graphql", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task UpdateCategory_Should_Return_Categories()
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
                //dbContext.Categories.RemoveRange(dbContext.Categories);
                dbContext.Categories.AddRange(expectedCategories);
                dbContext.SaveChanges();
            }
            var categoryId = expectedCategories[0].CategoryId;
            var query = @"
                mutation {
                    updateCategory(request: {categoryId: """ + categoryId + @""", name: ""AAA""}) {
                      categoryId
                      name
                    }
                }";
            var request = new
            {
                query
            };
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(request),
                System.Text.Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/graphql", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task DeleteCategory_Should_Return_Categories()
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
                //dbContext.Categories.RemoveRange(dbContext.Categories);
                dbContext.Categories.AddRange(expectedCategories);
                dbContext.SaveChanges();
            }
            var categoryId = expectedCategories[0].CategoryId;
            var query = @"
                mutation {
                    deleteCategory(id: """ + categoryId + @""") {
                      categoryId
                      name
                    }
                }";
            var request = new
            {
                query
            };
            var content = new StringContent(SpanJson.JsonSerializer.Generic.Utf16.Serialize(request),
                System.Text.Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/graphql", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().NotBeNullOrEmpty();
        }
    }
}
