using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task.Service.API.Resources
{
        public record SaveTaskResource
        {
                [Required]
                public string Name { get; set; }

                [Required]
                public string Description { get; set; }

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
                public int InstructorId { get; set; }

                [Required]
                public IEnumerable<SaveCriterionResource> Criteria { get; set; }
        }
}