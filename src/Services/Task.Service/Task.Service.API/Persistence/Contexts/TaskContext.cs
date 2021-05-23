using Microsoft.EntityFrameworkCore;
using Task.Service.API.Domain.Models;
using Task.Service.API.Persistence.EntityConfigurations;

namespace Task.Service.API.Persistence.Contexts
{
        public class TaskContext : DbContext
        {
                public DbSet<Domain.Models.Task> Tasks { get; set; }
                public DbSet<Criterion> Criteria { get; set; }

                public TaskContext(DbContextOptions<TaskContext> options) : base(options)
                {
                }

                protected override void OnModelCreating(ModelBuilder builder)
                {
                        builder.ApplyConfiguration(new TaskEntityTypeConfiguration());
                        builder.ApplyConfiguration(new CriterionEntityTypeConfiguration());
                }
        }
}