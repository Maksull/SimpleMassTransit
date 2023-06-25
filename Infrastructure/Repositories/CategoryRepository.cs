using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private readonly ApiDataContext _context;

        private static readonly Func<ApiDataContext, IEnumerable<Category>> GetCategories = 
            EF.CompileQuery((ApiDataContext context) => context.Categories);

        private static readonly Func<ApiDataContext, Guid, Task<Category?>> GetCategoryByGuid =
            EF.CompileAsyncQuery((ApiDataContext context, Guid id) => context.Categories.Find(id));

        public CategoryRepository(ApiDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => GetCategories(_context);

        public IQueryable<Category> QueryableCategories => _context.Categories;


        public async Task<Category?> GetCategoryById(Guid id) => await _context.Categories.FindAsync(id);

        public async Task CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
