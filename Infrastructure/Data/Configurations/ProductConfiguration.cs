using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        private readonly Guid[] _guids;

        public ProductConfiguration(Guid[] guids)
        {
            _guids = guids;
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            Product[] products =
           {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Kayak",
                    Price = 275,
                    CategoryId = _guids[0],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Lifejacket",
                    Price = 48.95m,
                    CategoryId = _guids[0],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Ball",
                    Price = 19.50m,
                    CategoryId = _guids[1],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Corner Flags",
                    Price = 34.95m,
                    CategoryId = _guids[1],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Stadium",
                    Price = 79500,
                    CategoryId = _guids[1],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Thinking Cap",
                    Price = 16,
                    CategoryId = _guids[2],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Unsteady Chair",
                    Price = 29.95m,
                    CategoryId = _guids[2],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Human Chess Board",
                    Price = 75,
                    CategoryId = _guids[2],
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "T-shirt",
                    Price = 1200,
                    CategoryId = _guids[2],
                }
            };

            builder.HasData(products);
        }
    }
}
