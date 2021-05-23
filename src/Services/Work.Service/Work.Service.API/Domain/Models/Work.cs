using System;

namespace Work.Service.API.Domain.Models
{
        public class Work
        {
                public int Id { get; set; }
                public string Content { get; set; }
                public DateTimeOffset? Submitted { get; set; }
                public DateTimeOffset? Modified { get; set; }

                public int AuthorId { get; set; }

                public int WorksDeadlineId { get; set; }
                public WorksDeadline WorksDeadline { get; set; }
        }
}