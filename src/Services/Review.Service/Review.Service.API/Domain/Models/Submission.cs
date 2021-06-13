using System;

namespace Review.Service.API.Domain.Models
{
        public class Submission
        {
                public int Id { get; set; }
                public string Content { get; set; }

                public string AuthorId { get; set; }

                public Guid WorkshopUid { get; set; }
        }
}