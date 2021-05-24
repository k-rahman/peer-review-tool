using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task.Service.API.Domain.Models;

namespace Task.Service.API.Persistence.EntityConfigurations
{
        class ParticipantEntityTypeConfiguration
            : IEntityTypeConfiguration<Participant>
        {
                public void Configure(EntityTypeBuilder<Participant> builder)
                {
                        builder.ToTable("participant");

                        builder.HasKey(participant => participant.Id);

                        builder.Property(participant => participant.Id)
                              .HasColumnName("id");

                        builder.Property(participant => participant.FirstName)
                              .HasColumnName("first_name")
                              .HasMaxLength(50)
                              .IsRequired();

                        builder.Property(participant => participant.LastName)
                              .HasColumnName("last_name")
                              .HasMaxLength(50)
                              .IsRequired();

                        builder.Property(participant => participant.TaskId)
                              .HasColumnName("task_id")
                              .IsRequired();
                }
        }
}