using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Service.API.Persistence.EntityConfigurations
{
        class CriterionEntityTypeConfiguration
            : IEntityTypeConfiguration<Domain.Models.Criterion>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Criterion> builder)
                {
                        builder.ToTable("criteria");

                        builder.HasKey(criterion => criterion.Id);

                        builder.Property(criterion => criterion.Id)
                            .HasColumnName("id");

                        builder.Property(criterion => criterion.Description)
                            .HasColumnName("description")
                            .IsRequired();

                        builder.Property(criterion => criterion.MaxPoints)
                            .HasColumnName("max_points");

                        builder.Property(criterion => criterion.TaskId)
                            .HasColumnName("task_id");
                }
        }
}