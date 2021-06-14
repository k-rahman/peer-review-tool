using System;
using System.Collections.Generic;

namespace Review.Service.API.Resources
{
        public record ReviewResource
        {
                public int Id { get; set; }
                public DateTimeOffset Created { get; set; }
                public DateTimeOffset? Modified { get; set; }
                public string Content { get; set; }

                public IEnumerable<CriterionResource> Criteria { get; set; }

        }
}