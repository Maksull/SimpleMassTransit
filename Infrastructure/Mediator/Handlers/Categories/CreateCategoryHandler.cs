using Core.Entities;
using Core.Mediator.Commands.Categories;
using Infrastructure.Mapperly;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Categories
{
    public sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly ICategoryService _categoryService;
        private readonly IBus _bus;

        public CreateCategoryHandler(ICategoryService categoryService, IBus bus)
        {
            _categoryService = categoryService;
            _bus = bus;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.CreateCategory(request.Category);

            var categoryCreated = CategoryMapper.CategoryToCategoryCreated(category);

            await _bus.Publish(categoryCreated);

            return category;
        }
    }
}
