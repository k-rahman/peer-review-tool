using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Work.Service.API.Presistence.EntityConfigurations
{
        public class WorkEntityTypeConfiguration :
        IEntityTypeConfiguration<Domain.Models.Work>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Work> builder)
                {
                        builder.ToTable("Works");

                        builder.HasKey(work => work.Id);

                        builder.Property(work => work.Id)
                              .IsRequired();

                        builder.Property(work => work.Content)
                        .IsRequired(false);

                        builder.Property(work => work.Submitted)
                        .IsRequired(false);

                        builder.Property(work => work.Modified)
                        .IsRequired(false);
                }
        }
}