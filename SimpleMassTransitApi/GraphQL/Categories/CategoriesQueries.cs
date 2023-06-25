using Core.Entities;
using Core.Mediator.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.GraphQL.Categories
{
    [ExtendObjectType("Query")]
    public sealed class CategoriesQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Category>> ReadCategories([FromServices] IMediator mediator)
        {
            return await mediator.Send(new GetQueryableCategoriesQuery());
        }
    }
}
