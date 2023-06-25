using Core.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IQueryable<Product> QueryableProducts { get; }
        Task<Product?> GetProductById(Guid id);
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
