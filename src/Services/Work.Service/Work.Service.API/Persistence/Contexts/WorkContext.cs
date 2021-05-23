using Microsoft.EntityFrameworkCore;
using Work.Service.API.Domain.Models;
using Work.Service.API.Presistence.EntityConfigurations;

namespace Work.Service.API.Persistence.Contexts
{
        public class WorkContext : DbContext
        {
                public DbSet<Domain.Models.Work> Works { get; set; }
                public DbSet<WorksDeadline> WorksDealines { get; set; }
                public WorkContext(DbContextOptions<WorkContext> options) : base(options)
                {
                }

                protected override void OnModelCreating(ModelBuilder builder)
                {
                        builder.ApplyConfiguration(new WorkEntityTypeConfiguration());
                        builder.ApplyConfiguration(new WorksDeadlineEntityTypeConfiguration());
                }
        }
}