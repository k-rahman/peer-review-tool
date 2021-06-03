using Microsoft.EntityFrameworkCore;
using Review.Service.API.Domain.Models;
using Review.Service.API.Persistence.EntityConfigurations;

namespace Review.Service.API.Persistence.Contexts
{
        public class ReviewContext : DbContext
        {
                public DbSet<Domain.Models.Review> Reviews { get; set; }
                public DbSet<Criterion> Criteria { get; set; }
                public DbSet<Work> Works { get; set; }
                public ReviewContext(DbContextOptions<ReviewContext> options) : base(options)
                {
                }

                protected override void OnModelCreating(ModelBuilder builder)
                {
                        builder.ApplyConfiguration(new ReviewEntityTypeConfiguration());
                        builder.ApplyConfiguration(new WorkEntityTypeConfiguration());
                        builder.ApplyConfiguration(new CriterionEntityTypeConfiguration());

                }
        }
}