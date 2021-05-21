using Microsoft.EntityFrameworkCore;
using Work.Service.API.Domain.Models;
using Work.Service.API.Presistence.EntityConfigurations;

namespace Work.Service.API.Presistence.Contexts
{
        public class WorkContext : DbContext
        {
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