using Core.Contracts.Controllers.Categories;
using Core.Entities;

namespace Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        IQueryable<Category> GetQueryableCategories();
        Task<Category?> GetCategory(Guid id);
        Task<Category> CreateCategory(CreateCategoryRequest createCategory);
        Task<Category?> UpdateCategory(UpdateCategoryRequest updateCategory);
        Task<Category?> DeleteCategory(Guid id);
    }
}
