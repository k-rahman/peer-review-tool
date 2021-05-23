using System;
using System.Collections.Generic;

namespace Task.Service.API.Resources
{
        public record TaskResource
        {
                public int Id { get; set; }
                public Guid Uid { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
                public DateTimeOffset ReviewStart { get; set; }
                public DateTimeOffset ReviewEnd { get; set; }
                public DateTimeOffset Published { get; set; }
                public DateTimeOffset Created { get; set; }
                public DateTimeOffset? Modified { get; set; }
                public int Instructor_id { get; set; }
                public IEnumerable<CriterionResource> Criteria { get; set; }
        }
}