using Core.Contracts.Controllers.Products;
using Core.Entities;
using Core.Mediator.Commands.Products;
using HotChocolate.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.GraphQL.Products
{
    [ExtendObjectType("Mutation")]
    public sealed class ProductsMutations
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Product> CreateProduct([FromServices] IMediator mediator, [FromServices] ITopicEventSender sender, CreateProductRequest request)
        {
            var product = await mediator.Send(new CreateProductCommand(request));

            await sender.SendAsync(nameof(ProductsSubscriptions.OnProductsChanges), product);

            return product;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Product?> UpdateProduct([FromServices] IMediator mediator, [FromServices] ITopicEventSender sender, UpdateProductRequest request)
        {
            var product = await mediator.Send(new UpdateProductCommand(request));

            await sender.SendAsync(nameof(ProductsSubscriptions.OnProductsChanges), product);

            return product;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Product?> DeleteProduct([FromServices] IMediator mediator, [FromServices] ITopicEventSender sender, Guid id)
        {
            var product = await mediator.Send(new DeleteProductCommand(id));

            await sender.SendAsync(nameof(ProductsSubscriptions.OnProductsChanges), product);

            return product;
        }
    }
}
