using Core.Contracts.Controllers.Products;
using Core.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _unitOfWork.Product.Products;
        }

        public IQueryable<Product> GetQueryableProducts()
        {
            return _unitOfWork.Product.QueryableProducts;
        }

        public async Task<Product?> GetProduct(Guid id)
        {
            var product = await _unitOfWork.Product.GetProductById(id);

            return product;
        }

        public async Task<Product> CreateProduct(CreateProductRequest createProduct)
        {
            //var mapper = new ProductMapper();
            //var product = mapper.CreateProductRequestToProduct(createProduct);

            //var product = new Product()
            //{
            //    Name = createProduct.Name,
            //    Price = createProduct.Price,
            //    CategoryId = createProduct.CategoryId,
            //};

            var product = ProductMapper.CreateProductRequestToProduct(createProduct);

            await _unitOfWork.Product.CreateProductAsync(product);

            return product;
        }

        public async Task<Product?> UpdateProduct(UpdateProductRequest updateProduct)
        {
            //var mapper = new ProductMapper();
            //var product = mapper.UpdateProductRequestToProduct(updateProduct);

            //var product = new Product()
            //{
            //    ProductId = updateProduct.ProductId,
            //    Name = updateProduct.Name,
            //    Price = updateProduct.Price,
            //    CategoryId = updateProduct.CategoryId,
            //};

            var t = await _unitOfWork.Product.QueryableProducts.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == updateProduct.ProductId);

            if (t is not null)
            {
                var product = ProductMapper.UpdateProductRequestToProduct(updateProduct);

                await _unitOfWork.Product.UpdateProductAsync(product);

                return product;
            }

            return null;
        }

        public async Task<Product?> DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.Product.GetProductById(id);

            if (product is not null)
            {
                await _unitOfWork.Product.DeleteProductAsync(product);
            }

            return product;
        }
    }
}
