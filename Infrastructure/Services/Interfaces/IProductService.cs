using Core.Contracts.Controllers.Products;
using Core.Entities;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        IQueryable<Product> GetQueryableProducts();
        Task<Product?> GetProduct(Guid id);
        Task<Product> CreateProduct(CreateProductRequest createProduct);
        Task<Product?> UpdateProduct(UpdateProductRequest updateProduct);
        Task<Product?> DeleteProduct(Guid id);
    }
}
