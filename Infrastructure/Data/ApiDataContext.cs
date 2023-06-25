using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public sealed class ApiDataContext : DbContext
    {
        public ApiDataContext(DbContextOptions<ApiDataContext> opts) : base(opts) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Guid[] categoryGuids = { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            builder.ApplyConfiguration(new CategoryConfiguration(categoryGuids));
            builder.ApplyConfiguration(new ProductConfiguration(categoryGuids));
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
