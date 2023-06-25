using Core.Entities;
using Core.Mediator.Queries.Categories;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Categories
{
    public sealed class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
    {
        private readonly ICategoryService _categoryService;
        private readonly IBus _bus;

        public GetCategoryByIdHandler(ICategoryService categoryService, IBus bus)
        {
            _categoryService = categoryService;
            _bus = bus;
        }

        public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetCategory(request.Id);

            if (category != null)
            {
                await _bus.Publish(category);
            }

            return category;
        }
    }
}
