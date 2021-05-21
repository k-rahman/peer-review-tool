using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Service.API.Persistence.EntityConfigurations
{
        class CriterionEntityTypeConfiguration
            : IEntityTypeConfiguration<Domain.Models.Criterion>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Criterion> builder)
                {
                        builder.ToTable("Criteria");

                        builder.HasKey(criterion => criterion.Id);

                        builder.Property(criterion => criterion.Id)
                            .IsRequired();

                        builder.Property(criterion => criterion.Description)
                            .IsRequired();

                        builder.Property(criterion => criterion.MaxPoints)
                        .IsRequired();
                }
        }
}