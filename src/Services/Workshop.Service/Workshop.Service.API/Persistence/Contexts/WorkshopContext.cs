using Microsoft.EntityFrameworkCore;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Persistence.EntityConfigurations;

namespace Workshop.Service.API.Persistence.Contexts
{
        public class WorkshopContext : DbContext
        {
                public DbSet<Domain.Models.Workshop> Workshops { get; set; }
                public DbSet<Criterion> Criteria { get; set; }
                public DbSet<Participant> Participants { get; set; }

                public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options)
                {
                }

                protected override void OnModelCreating(ModelBuilder builder)
                {
                        builder.ApplyConfiguration(new WorkshopEntityTypeConfiguration());
                        builder.ApplyConfiguration(new CriterionEntityTypeConfiguration());
                        builder.ApplyConfiguration(new ParticipantEntityTypeConfiguration());
                }
        }
}