using System;

namespace Work.Service.API.Resources
{
        public record WorkResource
        {
                public int Id { get; set; }
                public string Content { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
                public DateTimeOffset? Submitted { get; set; }
                public DateTimeOffset? Modified { get; set; }
        }
}