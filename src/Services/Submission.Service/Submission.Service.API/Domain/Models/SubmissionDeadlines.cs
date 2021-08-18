using System;
using System.Collections.Generic;

namespace Submission.Service.API.Domain.Models
{
        public class SubmissionDeadlines
        {
                public int Id { get; set; }
                public Guid Uid { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
                public string InstructorId { get; set; }

                // public ICollection<Submission> Submissions { get; set; }
        }
}