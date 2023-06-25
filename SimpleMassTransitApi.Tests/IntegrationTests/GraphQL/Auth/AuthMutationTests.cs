using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Runtime.Serialization;

namespace SimpleMassTransitApi.Tests.IntegrationTests.GraphQL.Auth
{
    public sealed class AuthMutationTests
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public AuthMutationTests()
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
                            options.UseInMemoryDatabase("InMemoryAuthMutations");
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Login_Should_Return_Jwt()
        {
            // Arrange
            var query = @"
                mutation {
                    login(request: {name: ""Admin"", password: ""Admin""}) {
                      jwt
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
            var jwt = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<LoginQueryResponse>(responseContent).Data.Login.Jwt;

            //var responseObject = System.Text.Json.JsonSerializer.Deserialize<JsonDocument>(responseContent);

            //var jwt = responseObject.RootElement
            //    .GetProperty("data")
            //    .GetProperty("login")
            //    .GetProperty("jwt")
            //    .GetString();


            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            jwt.Should().NotBeNullOrEmpty();
        }


        private struct LoginQueryResponse
        {
            [DataMember(Name = "data")]
            public DataResponse Data { get; set; }

            public struct DataResponse
            {
                [DataMember(Name = "login")]
                public LoginResponse Login { get; set; }

                public struct LoginResponse
                {
                    [DataMember(Name = "jwt")]
                    public string Jwt { get; set; }
                }
            }
        }
    }
}
