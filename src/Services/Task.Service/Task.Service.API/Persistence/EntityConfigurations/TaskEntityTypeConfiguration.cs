using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Service.API.Persistence.EntityConfiguration
{
        class TaskEntityTypeConfiguration
            : IEntityTypeConfiguration<Domain.Models.Task>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Task> builder)
                {
                        builder.ToTable("Tasks");

                        builder.HasKey(task => task.Id);

                        builder.HasIndex(task => task.Id)
                            .IsUnique();

                        builder.Property(task => task.Id)
                            .HasColumnType("uuid")
                            .HasDefaultValueSql("uuid_generate_v4()")
                            .IsRequired();

                        builder.Property(task => task.Name)
                            .IsRequired()
                            .HasMaxLength(255);

                        builder.Property(task => task.Description)
                            .IsRequired();

                        builder.Property(task => task.Link)
                            .IsRequired()
                            .HasMaxLength(255);

                        builder.Property(task => task.SubmissionStart)
                              .IsRequired();

                        builder.Property(task => task.SubmissionEnd)
                              .IsRequired();

                        builder.Property(task => task.ReviewStart)
                              .IsRequired();

                        builder.Property(task => task.ReviewEnd)
                              .IsRequired();

                        builder.Property(task => task.Published)
                              .IsRequired();

                        builder.Property(task => task.Created)
                              .IsRequired();

                        builder.Property(task => task.Modified)
                              .IsRequired();

                        builder.HasMany(task => task.Criteria)
                            .WithOne()
                            .HasForeignKey("TaskId")
                            .IsRequired()
                            .OnDelete(DeleteBehavior.NoAction);
                }
        }
}