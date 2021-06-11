using System;

namespace Submission.Service.API.Domain.Models
{
        public class Submission
        {
                public int Id { get; set; }
                public string Content { get; set; }
                public DateTimeOffset? Submitted { get; set; }
                public DateTimeOffset? Modified { get; set; }

                public string AuthorId { get; set; }

                public int SubmissionDeadlinesId { get; set; }
                public SubmissionDeadlines SubmissionDeadlines { get; set; }
        }
}