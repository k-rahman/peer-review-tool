using System;
using System.ComponentModel.DataAnnotations;

namespace Task.Service.API.Models
{
  public class Task
  {
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [StringLength(255)]
    public string Link { get; set; }
    public DateTimeOffset Publish { get; set; }
    public DateTimeOffset SubmissionStart { get; set; }
    public DateTimeOffset SubmissionEnd { get; set; }
    public DateTimeOffset ReviewStart { get; set; }
    public DateTimeOffset ReviewEnd { get; set; }
    public int InstructorId { get; set; }
  }
}