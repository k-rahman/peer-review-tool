using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Persistence.EntityConfigurations
{
        class GradeEntityTypeConfiguration
            : IEntityTypeConfiguration<Grade>
        {
                public void Configure(EntityTypeBuilder<Grade> builder)
                {
                        builder.ToTable("grades");

                        builder.HasKey(grade => new { grade.Review, grade.CriterionId });

                        builder.Property(grade => grade.Points)
                                .HasColumnName("points");

                        builder.Property(grade => grade.Feedback)
                                .HasColumnName("feedback");
                }
        }
}