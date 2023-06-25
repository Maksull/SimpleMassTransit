using Core.Entities;

namespace SimpleMassTransitApi.GraphQL.Categories
{
    [ExtendObjectType("Subscription")]
    public sealed class CategoriesSubscriptions
    {
        [Subscribe]
        [UseFiltering]
        [UseSorting]
        public Category OnCategoriesChanges([EventMessage] Category category)
        {
            return category;
        }
    }
}
