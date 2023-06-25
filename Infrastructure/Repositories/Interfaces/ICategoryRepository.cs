using Core.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        IQueryable<Category> QueryableCategories { get; }
        Task<Category?> GetCategoryById(Guid id);
        Task CreateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
    }
}
