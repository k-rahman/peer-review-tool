using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Task.Service.API.Domain.Models;

namespace Task.Service.API.Resources
{
        public record SaveTaskResource
        {
                [Required]
                public string Name { get; set; }

                [Required]
                public string Description { get; set; }

                [Required]
                public string Link { get; set; }

                [Required]
                public DateTimeOffset SubmissionStart { get; set; }

                [Required]
                public DateTimeOffset SubmissionEnd { get; set; }

                [Required]
                public DateTimeOffset ReviewStart { get; set; }

                [Required]
                public DateTimeOffset ReviewEnd { get; set; }

                [Required]
                public DateTimeOffset Published { get; set; }

                [Required]
                public Guid InstructorId { get; set; }

                [Required]
                public IEnumerable<Criterion> Criteria { get; set; }
        }
}