using Core.Contracts.Controllers.Categories;
using Core.Entities;
using Core.Mediator.Commands.Categories;
using Core.Mediator.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleMassTransitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());

            if (categories.Any())
            {
                return Ok(categories);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));

            if (category is not null)
            {
                return Ok(category);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest createCategory)
        {
            var category = await _mediator.Send(new CreateCategoryCommand(createCategory));

            //if (category is not null)
            //{
                return Ok();
            //}

            //return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        public async Task<IActionResult> UpdateProduct(UpdateCategoryRequest updateCategory)
        {
            var category = await _mediator.Send(new UpdateCategoryCommand(updateCategory));

            if (category is not null)
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
            var category = await _mediator.Send(new DeleteCategoryCommand(id));

            if (category is not null)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
