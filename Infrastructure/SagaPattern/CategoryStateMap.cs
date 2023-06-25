using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SagaPattern
{
    /*public class CategoryStateMap : SagaClassMap<CategoryState>
    {
        protected override void Configure(EntityTypeBuilder<CategoryState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CategoryId).IsRequired();
            entity.Property(x => x.CurrentState).HasMaxLength(50);

            entity.ToTable("CategoryState"); // Customize the table name if needed

            // Add any additional column mappings or constraints as required
        }
    }*/
}
