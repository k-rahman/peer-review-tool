using System;

namespace Submission.Service.API.Resources
{
        public class SubmissionDeadlinesResource
        {
                public int Id { get; set; }
                public Guid Uid { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
        }
}