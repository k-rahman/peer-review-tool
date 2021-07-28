using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Submission.Service.API.Persistence.EntityConfigurations
{
        public class SubmissionEntityTypeConfiguration :
        IEntityTypeConfiguration<Domain.Models.Submission>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Submission> builder)
                {
                        builder.ToTable("submissions");

                        builder.HasKey(submission => submission.Id);

                        builder.Property(submission => submission.Id)
                                .HasColumnName("id");

                        builder.Property(submission => submission.Content)
                               .HasColumnName("content");

                        builder.Property(submission => submission.Submitted)
                                .HasColumnName("submitted");

                        builder.Property(submission => submission.Modified)
                                .HasColumnName("modified");

                        builder.Property(submission => submission.AuthorId)
                                .HasColumnName("author_id");

                        builder.Property(submission => submission.Author)
                                                        .HasColumnName("author")
                                                        .HasMaxLength(255)
                                                        .IsRequired();

                        builder.Property(submission => submission.WorkshopUid)
                                                        .HasColumnName("workshop_uid")
                                                        .HasColumnType("uuid")
                                                        .IsRequired();

                        // builder.Property(submission => submission.SubmissionDeadlinesId)
                        // 				.HasColumnName("submission_deadlines_id");
                }
        }
}