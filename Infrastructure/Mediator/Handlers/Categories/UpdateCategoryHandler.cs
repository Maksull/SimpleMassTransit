using Core.Entities;
using Core.Mediator.Commands.Categories;
using Infrastructure.Mapperly;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Categories
{
    public sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category?>
    {
        private readonly ICategoryService _categoryService;
        private readonly IBus _bus;

        public UpdateCategoryHandler(ICategoryService categoryService, IBus bus)
        {
            _categoryService = categoryService;
            _bus = bus;
        }

        public async Task<Category?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.UpdateCategory(request.Category);

            if(category is not null)
            {
                var categoryUpdated = CategoryMapper.CategoryToCategoryUpdated(category);

                await _bus.Publish(categoryUpdated);
            }

            return category;
        }
    }
}
