using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Service.API.Persistence.EntityConfigurations
{
        class TaskEntityTypeConfiguration
            : IEntityTypeConfiguration<Domain.Models.Task>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Task> builder)
                {
                        builder.ToTable("tasks");

                        builder.HasKey(task => task.Id);

                        builder.Property(task => task.Id)
                              .HasColumnName("id");

                        builder.HasIndex(task => task.Uid)
                              .IsUnique();

                        builder.Property(task => task.Uid)
                              .HasColumnName("uid")
                              .HasColumnType("uuid")
                              .IsRequired();

                        builder.Property(task => task.Name)
                              .HasColumnName("name")
                              .IsRequired()
                              .HasMaxLength(255);

                        builder.Property(task => task.Description)
                              .HasColumnName("description")
                              .IsRequired();

                        builder.Property(task => task.SubmissionStart)
                              .HasColumnName("submission_start");

                        builder.Property(task => task.SubmissionEnd)
                              .HasColumnName("submission_end");

                        builder.Property(task => task.ReviewStart)
                              .HasColumnName("review_start");

                        builder.Property(task => task.ReviewEnd)
                              .HasColumnName("review_end");

                        builder.Property(task => task.Published)
                              .HasColumnName("published");

                        builder.Property(task => task.Created)
                              .HasColumnName("created");

                        builder.Property(task => task.Modified)
                              .HasColumnName("modified");

                        // builder.HasMany(task => task.Criteria)
                        //     .WithOne()
                        //     .HasForeignKey("TaskId")
                        //     .IsRequired();
                }
        }
}