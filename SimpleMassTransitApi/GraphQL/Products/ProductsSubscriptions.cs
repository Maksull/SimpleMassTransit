using Core.Entities;

namespace SimpleMassTransitApi.GraphQL.Products
{
    [ExtendObjectType("Subscription")]
    public sealed class ProductsSubscriptions
    {
        [Subscribe]
        [UseFiltering]
        [UseSorting]
        public Product OnProductsChanges([EventMessage] Product product)
        {
            return product;
        }
    }
}
