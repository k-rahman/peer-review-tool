using System;
using System.ComponentModel.DataAnnotations;

namespace Submission.Service.API.Resources
{
        public record SaveSubmissionResource
        {
                [Required]
                public string Content { get; set; }

                [Required]
                public int WorkshopId { get; set; }
        }
}