using Microsoft.EntityFrameworkCore;
using Submission.Service.API.Domain.Models;
using Submission.Service.API.Presistence.EntityConfigurations;

namespace Submission.Service.API.Persistence.Contexts
{
        public class SubmissionContext : DbContext
        {
                public DbSet<Domain.Models.Submission> Submissions { get; set; }
                public DbSet<SubmissionDeadlines> SubmissionsDealines { get; set; }
                public SubmissionContext(DbContextOptions<SubmissionContext> options) : base(options)
                {
                }

                protected override void OnModelCreating(ModelBuilder builder)
                {
                        builder.ApplyConfiguration(new SubmissionEntityTypeConfiguration());
                        builder.ApplyConfiguration(new SubmissionsDeadlineEntityTypeConfiguration());
                }
        }
}