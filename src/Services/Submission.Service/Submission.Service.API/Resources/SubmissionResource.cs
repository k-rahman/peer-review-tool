using System;

namespace Submission.Service.API.Resources
{
        public record SubmissionResource
        {
                public int Id { get; set; }
                public string Content { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
                public DateTimeOffset? Submitted { get; set; }
                public DateTimeOffset? Modified { get; set; }
        }
}