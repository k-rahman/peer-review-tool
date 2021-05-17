using System;
using System.Collections.Generic;

namespace Task.Service.API.Resources
{
        public record TaskResource
        {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public string Link { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
                public DateTimeOffset ReviewStart { get; set; }
                public DateTimeOffset ReviewEnd { get; set; }
                public DateTimeOffset Published { get; set; }
                public DateTimeOffset Created { get; set; }
                public DateTimeOffset Modified { get; set; }
                public Guid Instructor_id { get; set; }
                public IEnumerable<CriterionResource> Criteria { get; set; }
        }
}