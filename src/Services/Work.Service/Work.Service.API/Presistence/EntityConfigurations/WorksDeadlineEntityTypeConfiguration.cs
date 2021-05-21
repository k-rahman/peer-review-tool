using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Work.Service.API.Domain.Models;

namespace Work.Service.API.Presistence.EntityConfigurations
{
        public class WorksDeadlineEntityTypeConfiguration :
        IEntityTypeConfiguration<WorksDeadline>
        {
                public void Configure(EntityTypeBuilder<WorksDeadline> builder)
                {
                        builder.ToTable("WorksDeadlines");

                        builder.HasKey(worksDeadline => worksDeadline.Id);

                        builder.Property(worksDeadline => worksDeadline.Id)
                              .IsRequired();

                        builder.Property(worksDeadline => worksDeadline.Link)
                                .HasColumnType("uuid")
                              .IsRequired();

                        builder.Property(worksDeadline => worksDeadline.SubmissionStart)
                              .IsRequired();

                        builder.Property(worksDeadline => worksDeadline.SubmissionEnd)
                              .IsRequired();

                        builder.HasMany(worksDeadline => worksDeadline.Works)
                            .WithOne()
                            .HasForeignKey("WorksDeadlineId")
                            .IsRequired();
                }
        }
}