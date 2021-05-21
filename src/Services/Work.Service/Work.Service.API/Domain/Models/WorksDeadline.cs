using System;
using System.Collections.Generic;

namespace Work.Service.API.Domain.Models
{
        public class WorksDeadline
        {
                public int Id { get; set; }
                public Guid Link { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
                public ICollection<Work> Works { get; set; }
        }
}