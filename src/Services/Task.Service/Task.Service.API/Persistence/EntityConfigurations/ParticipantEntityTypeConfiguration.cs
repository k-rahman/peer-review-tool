using System.Collections.Generic;
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
                        builder.ToTable("participants");

                        builder.HasKey(participant => participant.Id);

                        builder.Property(participant => participant.Id)
                              .HasColumnName("id");

                        builder.HasMany(participant => participant.Tasks)
                             .WithMany(task => task.Participants)
                             .UsingEntity<Dictionary<string, object>>(
                             "task_participants",
                             e => e
                                   .HasOne<Domain.Models.Task>()
                                   .WithMany()
                                   .HasForeignKey("task_id"),
                             e => e
                                   .HasOne<Participant>()
                                   .WithMany()
                                   .HasForeignKey("participant_id")
                              );
                }
        }
}