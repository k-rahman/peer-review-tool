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
                        builder.ToTable("works_deadlines");

                        builder.HasKey(worksDeadline => worksDeadline.Id);

                        builder.Property(worksDeadline => worksDeadline.Id)
                              .HasColumnName("id");

                        builder.Property(worksDeadline => worksDeadline.Uid)
                              .HasColumnName("Uid")
                              .HasColumnType("uuid")
                              .IsRequired();

                        builder.Property(worksDeadline => worksDeadline.SubmissionStart)
                              .HasColumnName("submission_start");

                        builder.Property(worksDeadline => worksDeadline.SubmissionEnd)
                              .HasColumnName("submission_end");
                }
        }
}