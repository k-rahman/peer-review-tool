using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Service.API.Domain.Models;

namespace Workshop.Service.API.Persistence.EntityConfigurations
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

                        builder.Property(participant => participant.Auth0Id)
                                                .HasColumnName("auth0_id")
                                                .HasMaxLength(255)
                                                .IsRequired();

                        builder.Property(participant => participant.Email)
                                                .HasColumnName("email")
                                                .HasMaxLength(255)
                                                .IsRequired();

                        builder.Property(participant => participant.Name)
                                                .HasColumnName("name")
                                                .HasMaxLength(255)
                                                .IsRequired();

                        builder.HasMany(participant => participant.Workshops)
                                         .WithMany(workshop => workshop.Participants)
                                         .UsingEntity<Dictionary<string, object>>(
                                         "workshop_participants",
                                         e => e
                                                .HasOne<Domain.Models.Workshop>()
                                                .WithMany()
                                                .HasForeignKey("workshop_id"),
                                         e => e
                                                .HasOne<Participant>()
                                                .WithMany()
                                                .HasForeignKey("participant_id")
                                                );
                }
        }
}