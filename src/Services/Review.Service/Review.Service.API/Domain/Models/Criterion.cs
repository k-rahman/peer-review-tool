using System;
using System.Collections.Generic;

namespace Review.Service.API.Domain.Models
{
        public class Criterion
        {
                public int Id { get; set; }
                public string Description { get; set; }
                public int MaxPoints { get; set; }

                public Guid WorkshopUid { get; set; }

                public ICollection<Review> Reviews { get; set; } = new List<Review>();
                public List<Grade> Grades { get; set; } = new List<Grade>();
        }
}