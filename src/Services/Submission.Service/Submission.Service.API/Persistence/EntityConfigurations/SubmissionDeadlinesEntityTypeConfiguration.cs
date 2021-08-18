using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Service.API.Domain.Models;

namespace Submission.Service.API.Persistence.EntityConfigurations
{
        public class SubmissionDeadlinesEntityTypeConfiguration :
        IEntityTypeConfiguration<SubmissionDeadlines>
        {
                public void Configure(EntityTypeBuilder<SubmissionDeadlines> builder)
                {
                        builder.ToTable("submission_deadlines");

                        builder.HasKey(submissionDeadlines => submissionDeadlines.Id);

                        builder.Property(submissionDeadlines => submissionDeadlines.Id)
                              .HasColumnName("id");

                        builder.Property(submissionDeadlines => submissionDeadlines.Uid)
                              .HasColumnName("uid")
                              .HasColumnType("uuid")
                              .IsRequired();

                        builder.Property(submissionDeadlines => submissionDeadlines.SubmissionStart)
                              .HasColumnName("submission_start");

                        builder.Property(submissionDeadlines => submissionDeadlines.SubmissionEnd)
                              .HasColumnName("submission_end");

                        builder.Property(submissionDeadlines => submissionDeadlines.InstructorId)
                              .HasColumnName("instructor_id")
                              .HasMaxLength(255)
                              .IsRequired();
                }
        }
}