using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Service.API.Persistence.EntityConfiguration
{
        class CriterionEntityTypeConfiguration
            : IEntityTypeConfiguration<Domain.Models.Criterion>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Criterion> builder)
                {
                        builder.ToTable("Criteria");

                        builder.HasKey(criterion => criterion.Id);

                        builder.HasIndex(criterion => criterion.Id)
                            .IsUnique();

                        builder.Property(criterion => criterion.Id)
                            .HasColumnType("uuid")
                            .HasDefaultValueSql("uuid_generate_v4()")
                            .IsRequired();

                        builder.Property(criterion => criterion.Description)
                            .IsRequired();

                        builder.Property(criterion => criterion.MaxPoints)
                        .IsRequired();
                }
        }
}