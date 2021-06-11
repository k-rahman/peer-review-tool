using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workshop.Service.API.Persistence.EntityConfigurations
{
        class WorkshopEntityTypeConfiguration
            : IEntityTypeConfiguration<Domain.Models.Workshop>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Workshop> builder)
                {
                        builder.ToTable("workshops");

                        builder.HasKey(workshop => workshop.Id);

                        builder.Property(workshop => workshop.Id)
                              .HasColumnName("id");

                        builder.HasIndex(workshop => workshop.Uid)
                              .IsUnique();

                        builder.Property(workshop => workshop.Uid)
                              .HasColumnName("uid")
                              .HasColumnType("uuid")
                              .IsRequired();

                        builder.Property(workshop => workshop.Name)
                              .HasColumnName("name")
                              .IsRequired()
                              .HasMaxLength(255);

                        builder.Property(workshop => workshop.Description)
                              .HasColumnName("description")
                              .IsRequired();

                        builder.Property(workshop => workshop.SubmissionStart)
                              .HasColumnName("submission_start");

                        builder.Property(workshop => workshop.SubmissionEnd)
                              .HasColumnName("submission_end");

                        builder.Property(workshop => workshop.ReviewStart)
                              .HasColumnName("review_start");

                        builder.Property(workshop => workshop.ReviewEnd)
                              .HasColumnName("review_end");

                        builder.Property(workshop => workshop.Published)
                              .HasColumnName("published");

                        builder.Property(workshop => workshop.Created)
                              .HasColumnName("created");

                        builder.Property(workshop => workshop.Modified)
                              .HasColumnName("modified");

                        builder.Property(workshop => workshop.InstructorId)
                              .HasColumnName("instructor_id")
                              .HasMaxLength(255)
                              .IsRequired();
                }
        }
}