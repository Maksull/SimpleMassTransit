using Core.Contracts.Controllers.Categories;
using Core.Entities;
using Core.Mediator.Commands.Categories;
using HotChocolate.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.GraphQL.Categories
{
    [ExtendObjectType("Mutation")]
    public sealed class CategoriesMutations
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Category> CreateCategory([FromServices] IMediator mediator, [FromServices] ITopicEventSender sender, CreateCategoryRequest request)
        {
            var category = await mediator.Send(new CreateCategoryCommand(request));

            await sender.SendAsync(nameof(CategoriesSubscriptions.OnCategoriesChanges), category);

            return category;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Category?> UpdateCategory([FromServices] IMediator mediator, [FromServices] ITopicEventSender sender, UpdateCategoryRequest request)
        {
            var category = await mediator.Send(new UpdateCategoryCommand(request));

            await sender.SendAsync(nameof(CategoriesSubscriptions.OnCategoriesChanges), category);

            return category;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Category?> DeleteCategory([FromServices] IMediator mediator, [FromServices] ITopicEventSender sender, Guid id)
        {
            var category = await mediator.Send(new DeleteCategoryCommand(id));

            if(category != null)
            {
                await sender.SendAsync(nameof(CategoriesSubscriptions.OnCategoriesChanges), category);
            }

            return category;
        }
    }
}
