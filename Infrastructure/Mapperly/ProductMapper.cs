using Core.Contracts.Consumers.Products;
using Core.Contracts.Controllers.Products;
using Core.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure
{
    [Mapper]
    //public partial class ProductMapper
    //{
    //    public partial Product CreateProductRequestToProduct(CreateProductRequest createProduct);
    //    public partial Product UpdateProductRequestToProduct(UpdateProductRequest createProduct);
    //}

    public static partial class ProductMapper
    {
        public static partial Product CreateProductRequestToProduct(CreateProductRequest createProduct);

        public static partial Product UpdateProductRequestToProduct(UpdateProductRequest updateProduct);

        public static partial ProductCreated ProductToProductCreated(Product product);

        public static ProductCreated MapCreateProductRequestToProductCreated(CreateProductRequest createProduct)
        {
            var productId = Guid.Empty;

            return new(productId, createProduct.Name, createProduct.Price, createProduct.CategoryId);
        }

        public static partial ProductUpdated UpdateProductRequestToProductUpdated(UpdateProductRequest updateProduct);

        public static partial ProductUpdated ProductToProductUpdated(Product product);

        public static partial ProductDeleted ProductToProductDeleted(Product product);


    }
}
