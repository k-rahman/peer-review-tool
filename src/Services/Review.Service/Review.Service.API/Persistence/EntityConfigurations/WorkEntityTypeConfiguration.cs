using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Persistence.EntityConfigurations
{
        public class WorkEntityTypeConfiguration :
        IEntityTypeConfiguration<Work>
        {
                public void Configure(EntityTypeBuilder<Work> builder)
                {
                        builder.ToTable("works");

                        builder.HasKey(work => work.Id);

                        builder.Property(work => work.Id)
                              .HasColumnName("id");

                        builder.Property(work => work.Content)
                              .HasColumnName("content");

                        builder.Property(work => work.AuthorId)
                              .HasColumnName("author_id");

                        builder.Property(work => work.TaskUid)
                              .HasColumnName("task_uid");
                }
        }
}