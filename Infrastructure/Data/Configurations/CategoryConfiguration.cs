using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly Guid[] _guids;
        public CategoryConfiguration(Guid[] guids)
        {
            _guids = guids;
        }

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Category[] categories = {
                new()
                {
                    CategoryId = _guids[0],
                    Name = "Watersports",
                },
                new()
                {
                    CategoryId = _guids[1],
                    Name = "Football",
                },
                new()
                {
                    CategoryId = _guids[2],
                    Name = "Chess",
                }
            };

            builder.HasData(categories);
        }
    }
}
