using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Persistence.EntityConfigurations
{
        class CriterionEntityTypeConfiguration
            : IEntityTypeConfiguration<Criterion>
        {
                public void Configure(EntityTypeBuilder<Criterion> builder)
                {
                        builder.ToTable("criteria");

                        builder.HasKey(criterion => criterion.Id);

                        builder.Property(criterion => criterion.Id)
                            .HasColumnName("id");

                        builder.Property(criterion => criterion.Description)
                            .HasColumnName("description")
                            .IsRequired();

                        builder.Property(criterion => criterion.MaxPoints)
                            .HasColumnName("max_points")
                            .IsRequired();

                        builder.Property(criterion => criterion.TaskUid)
                            .HasColumnName("task_uid")
                            .IsRequired();
                }
        }
}