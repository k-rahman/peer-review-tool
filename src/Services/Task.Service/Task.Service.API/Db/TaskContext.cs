using System;
using Microsoft.EntityFrameworkCore;

namespace Task.Service.API.Db
{
  public class TaskContext : DbContext
  {
    public DbSet<Models.Task> Tasks { get; set; }
    public TaskContext(DbContextOptions<TaskContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Models.Task>()
            .HasData(new
            {
              Id = 1,
              Name = "first task",
              Description = "first task description",
              Link = "first task link",
              Publish = DateTimeOffset.Now,
              SubmissionStart = DateTimeOffset.Now,
              SubmissionEnd = DateTimeOffset.Now,
              ReviewStart = DateTimeOffset.Now,
              ReviewEnd = DateTimeOffset.Now,
              InstructorId = 1
            },
            new
            {
              Id = 2,
              Name = "second task",
              Description = "second task description",
              Link = "second task link",
              Publish = DateTimeOffset.Now,
              SubmissionStart = DateTimeOffset.Now,
              SubmissionEnd = DateTimeOffset.Now,
              ReviewStart = DateTimeOffset.Now,
              ReviewEnd = DateTimeOffset.Now,
              InstructorId = 1
            });
    }
  }
}