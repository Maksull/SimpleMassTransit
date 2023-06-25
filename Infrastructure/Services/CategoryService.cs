using Core.Contracts.Controllers.Categories;
using Core.Entities;
using Infrastructure.Mapperly;
using Infrastructure.Services.Interfaces;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Category.Categories;
        }

        public IQueryable<Category> GetQueryableCategories()
        {
            return _unitOfWork.Category.QueryableCategories;
        }

        public async Task<Category?> GetCategory(Guid id)
        {
            var category = await _unitOfWork.Category.GetCategoryById(id);

            return category;
        }

        public async Task<Category> CreateCategory(CreateCategoryRequest createCategory)
        {
            //var category = new Category()
            //{
            //    Name = createCategory.Name,
            //};

            var category = CategoryMapper.CreateCategoryRequestToCategory(createCategory);

            await _unitOfWork.Category.CreateCategoryAsync(category);

            return category;
        }

        public async Task<Category?> UpdateCategory(UpdateCategoryRequest updateCategory)
        {
            //var category = new Category()
            //{
            //    CategoryId = updateCategory.CategoryId,
            //    Name = updateCategory.Name,
            //};

            var t = await _unitOfWork.Category.QueryableCategories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == updateCategory.CategoryId);

            if (t is not null)
            {
                var category = CategoryMapper.UpdateCategoryRequestToCategory(updateCategory);

                await _unitOfWork.Category.UpdateCategoryAsync(category);

                return category;
            }

            return null;
        }

        public async Task<Category?> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.Category.QueryableCategories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category is not null)
            {
                await _unitOfWork.Category.DeleteCategoryAsync(category);
            }

            return category;
        }

    }
}
