using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Work.Service.API.Presistence.EntityConfigurations
{
        public class WorkEntityTypeConfiguration :
        IEntityTypeConfiguration<Domain.Models.Work>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Work> builder)
                {
                        builder.ToTable("works");

                        builder.HasKey(work => work.Id);

                        builder.Property(work => work.Id)
                                .HasColumnName("id");

                        builder.Property(work => work.Content)
                               .HasColumnName("content");

                        builder.Property(work => work.Submitted)
                                .HasColumnName("submitted");

                        builder.Property(work => work.Modified)
                                .HasColumnName("modified");

                        builder.Property(work => work.AuthorId)
                                .HasColumnName("author_id");

                        builder.Property(work => work.WorksDeadlineId)
                                .HasColumnName("works_deadline_id");
                }
        }
}