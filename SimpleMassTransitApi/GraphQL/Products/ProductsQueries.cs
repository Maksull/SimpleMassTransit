using Core.Entities;
using Core.Mediator.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.GraphQL.Products
{
    [ExtendObjectType("Query")]
    public sealed class ProductsQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Product>> ReadProducts([FromServices] IMediator mediator)
        {
            return await mediator.Send(new GetQueryableProductsQuery());
        }
    }
}
