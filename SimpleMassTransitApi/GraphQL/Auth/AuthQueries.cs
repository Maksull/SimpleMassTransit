using Core.Entities;
using HotChocolate.Authorization;

namespace SimpleMassTransitApi.GraphQL.Auth
{
    [ExtendObjectType("Query")]
    public sealed class AuthQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public Task<Category> ReadAuth()
        {
            return Task.FromResult(new Category() { CategoryId = Guid.Empty, Name = "Dummy" });
        }
    }
}
