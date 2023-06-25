using Core.Contracts.Controllers.Categories;
using Core.Entities;
using Core.Mediator.Commands.Categories;
using Core.Mediator.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/mini/categories", GetCategories);
            app.MapGet("api/mini/categories/{id:guid}", GetCategory);
            app.MapPost("api/mini/categories", CreateCategory);
            app.MapPut("api/mini/categories", UpdateCategory);
            app.MapDelete("api/mini/categories/{id:guid}", DeleteCategory);

            return app;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public static async Task<IResult> GetCategories(IMediator mediator)
        {
            var categories = await mediator.Send(new GetCategoriesQuery());

            if (categories.Any())
            {
                return Results.Ok(categories);
            }

            return Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public static async Task<IResult> GetCategory(Guid id, IMediator mediator)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery(id));

            if (category is not null)
            {
                return Results.Ok(category);
            }

            return Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        public static async Task<IResult> CreateCategory(CreateCategoryRequest request, IMediator mediator)
        {
            var category = await mediator.Send(new CreateCategoryCommand(request));

            return Results.Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public static async Task<IResult> UpdateCategory(UpdateCategoryRequest request, IMediator mediator)
        {
            var category = await mediator.Send(new UpdateCategoryCommand(request));

            if (category is not null)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public static async Task<IResult> DeleteCategory(Guid id, IMediator mediator)
        {
            var category = await mediator.Send(new DeleteCategoryCommand(id));

            if (category is not null)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }

    }
}
