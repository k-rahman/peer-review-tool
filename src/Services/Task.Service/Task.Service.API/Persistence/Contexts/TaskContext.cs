using System;
using Microsoft.EntityFrameworkCore;
using Task.Service.API.Persistence.EntityConfiguration;

namespace Task.Service.API.Persistence.Context
{
        public class TaskContext : DbContext
        {
                public DbSet<Domain.Models.Task> Tasks { get; set; }

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