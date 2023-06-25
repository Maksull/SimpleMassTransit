using Core.Contracts.Controllers.Products;
using Core.Entities;
using Core.Mediator.Commands.Products;
using Core.Mediator.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/mini/products", GetProducts);
            app.MapGet("api/mini/products/{id:guid}", GetProduct);
            app.MapPost("api/mini/products", CreateProduct);
            app.MapPut("api/mini/products", UpdateProduct);
            app.MapDelete("api/mini/products/{id:guid}", DeleteProduct);

            return app;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async static Task<IResult> GetProducts(IMediator mediator)
        {
            var products = await mediator.Send(new GetProductsQuery());

            if (products.Any())
            {
                return Results.Ok(products);
            }

            return Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async static Task<IResult> GetProduct(Guid id, IMediator mediator)
        {
            var product = await mediator.Send(new GetProductByIdQuery(id));

            if (product is not null)
            {
                return Results.Ok(product);
            }

            return Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        public async static Task<IResult> CreateProduct(CreateProductRequest request, IMediator mediator)
        {
            var product = await mediator.Send(new CreateProductCommand(request));

            return Results.Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public static async Task<IResult> UpdateProduct(UpdateProductRequest request, IMediator mediator)
        {
            var product = await mediator.Send(new UpdateProductCommand(request));

            if (product is not null)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public static async Task<IResult> DeleteProduct(Guid id, IMediator mediator)
        {
            var product = await mediator.Send(new DeleteProductCommand(id));

            if (product is not null)
            {
                return Results.Ok();
            }

            return Results.NotFound();
        }
    }
}
