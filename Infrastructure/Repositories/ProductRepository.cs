using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly ApiDataContext _context;

        private static readonly Func<ApiDataContext, IEnumerable<Product>> GetProducts =
            EF.CompileQuery((ApiDataContext context) => context.Products);

        private static readonly Func<ApiDataContext, Guid, Task<Product?>> GetProductByGuid =
            EF.CompileAsyncQuery((ApiDataContext context, Guid id) => context.Products.Find(id));

        public ProductRepository(ApiDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products => GetProducts(_context);

        public IQueryable<Product> QueryableProducts => _context.Products;


        public async Task<Product?> GetProductById(Guid id) => await _context.Products.FindAsync(id);

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
