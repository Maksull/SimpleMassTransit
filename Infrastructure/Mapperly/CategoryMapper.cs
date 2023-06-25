using Core.Contracts.Consumers.Categories;
using Core.Contracts.Controllers.Categories;
using Core.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Mapperly
{
    [Mapper]
    public static partial class CategoryMapper
    {
        public static partial Category CreateCategoryRequestToCategory(CreateCategoryRequest createCategory);

        public static partial Category UpdateCategoryRequestToCategory(UpdateCategoryRequest updateCategory);

        public static partial CategoryCreated CategoryToCategoryCreated(Category category);

        public static CategoryCreated MapCreateCategoryRequestToCategoryCreated(CreateCategoryRequest createCategory)
        {
            var categoryId = Guid.Empty;

            return new(categoryId, createCategory.Name);
        }

        public static partial CategoryUpdated UpdateCategoryRequestToCategoryUpdated(UpdateCategoryRequest updateCategory);

        public static partial CategoryUpdated CategoryToCategoryUpdated(Category category);

        public static partial CategoryDeleted CategoryToCategoryDeleted(Category category);
    }
}
