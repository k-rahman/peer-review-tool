using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Persistence.EntityConfigurations
{
        public class SubmissionEntityTypeConfiguration :
        IEntityTypeConfiguration<Submission>
        {
                public void Configure(EntityTypeBuilder<Submission> builder)
                {
                        builder.ToTable("submissions");

                        builder.HasKey(submission => submission.Id);

                        builder.Property(submission => submission.Id)
                              .HasColumnName("id");

                        builder.Property(submission => submission.Content)
                              .HasColumnName("content");

                        builder.Property(submission => submission.AuthorId)
                              .HasColumnName("author_id");

                        builder.Property(submission => submission.WorkshopUid)
                              .HasColumnName("workshop_uid");
                }
        }
}