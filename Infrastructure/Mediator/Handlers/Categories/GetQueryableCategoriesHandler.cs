using Core.Entities;
using Core.Mediator.Queries.Categories;
using Infrastructure.Services.Interfaces;
using MediatR;

namespace Infrastructure.Mediator.Handlers.Categories
{
    public sealed class GetQueryableCategoriesHandler : IRequestHandler<GetQueryableCategoriesQuery, IQueryable<Category>>
    {
        private readonly ICategoryService _categoryService;

        public GetQueryableCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Task<IQueryable<Category>> Handle(GetQueryableCategoriesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_categoryService.GetQueryableCategories());
        }
    }
}
