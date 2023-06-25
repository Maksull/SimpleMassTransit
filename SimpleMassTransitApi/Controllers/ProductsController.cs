using Core.Contracts.Controllers.Products;
using Core.Entities;
using Core.Mediator.Commands.Products;
using Core.Mediator.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());

            if (products.Any())
            {
                return Ok(products);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product is not null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        public async Task<IActionResult> CreateProduct(CreateProductRequest createProduct)
        {
            var product = await _mediator.Send(new CreateProductCommand(createProduct));

            if (product is not null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest updateProduct)
        {
            var product = await _mediator.Send(new UpdateProductCommand(updateProduct));

            if (product is not null)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _mediator.Send(new DeleteProductCommand(id));

            if (product is not null)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
