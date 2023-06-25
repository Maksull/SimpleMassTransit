using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SagaPattern
{
    /*public class CategorySagaDataContext : SagaDbContext
    {
        public CategorySagaDataContext(DbContextOptions<CategorySagaDataContext> options) : base(options) { }


        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new CategoryStateMap(); }
        }

        public DbSet<CategoryState> CategoryStates { get; set; }
    }*/
}
