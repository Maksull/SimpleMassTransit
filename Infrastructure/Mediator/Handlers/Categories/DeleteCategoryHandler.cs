using Core.Entities;
using Core.Mediator.Commands.Categories;
using Infrastructure.Mapperly;
using Infrastructure.Services.Interfaces;
using MassTransit;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Categories
{
    public sealed class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Category?>
    {
        private readonly ICategoryService _categoryService;
        private readonly IBus _bus;

        public DeleteCategoryHandler(ICategoryService categoryService, IBus bus)
        {
            _categoryService = categoryService;
            _bus = bus;
        }

        public async Task<Category?> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryService.DeleteCategory(request.Id);

            if(category != null)
            {
                var categoryDeleted = CategoryMapper.CategoryToCategoryDeleted(category);

                await _bus.Publish(categoryDeleted);
            }

            return category;
        }
    }
}
