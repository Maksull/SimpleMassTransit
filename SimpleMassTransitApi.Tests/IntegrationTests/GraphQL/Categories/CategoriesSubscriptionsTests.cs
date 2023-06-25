using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL;
using System.Net;

namespace SimpleMassTransitApi.Tests.IntegrationTests.GraphQL.Categories
{
    public sealed class CategoriesSubscriptionsTests
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public CategoriesSubscriptionsTests()
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
        public async Task OnCategoriesChanges_Should_Return_Category()
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
                subscription {
                    onCategoriesChanges {
                      categoryId
                      name
                    }
                }";
            var request = new GraphQLRequest
            {
                Query = query
            };


            //using (var httpClient = _factory.CreateClient())
            //{
            //    var graphQLHttpClient = new GraphQLHttpClient(new GraphQLHttpClientOptions { EndPoint = new Uri("http://localhost/graphql", UriKind.Absolute) }, new SystemTextJsonSerializer(), httpClient);

            //    var subscriptionResult = graphQLHttpClient.CreateSubscriptionStream<Category>(request);
            //    var subscription = subscriptionResult.Subscribe(result =>
            //    {
            //        Console.WriteLine(result); // Print the received subscription result
            //    });
            //    var resss1 = graphQLHttpClient.SendMutationAsync<Category>(request2);
            //    subscriptionResult.Subscribe(result =>
            //                    {
            //                        if (result.Errors != null)
            //                        {
            //                            // Handle any errors in the result
            //                        }
            //                        else
            //                        {
            //                            var data = result.Data; // Access the data object
            //                            data.Name.Should().Be("AAGAGA");

            //                            // Process the received data as needed
            //                            // For example, if your subscription returns a category object:
            //                            //var category = data["onCategoriesChanges"].ToObject<Category>();
            //                            // Use the category object as needed
            //                        }
            //                    });

            //    var resss = graphQLHttpClient.SendMutationAsync<Category>(request2);

            //    await Task.Delay(TimeSpan.FromSeconds(10));

            //    // Assert
            //    var response = subscriptionResult.ToString();
            //    response.Should().NotBeNull();
            //}
        }
    }
}
