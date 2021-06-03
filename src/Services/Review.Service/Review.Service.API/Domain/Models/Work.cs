using System;

namespace Review.Service.API.Domain.Models
{
        public class Work
        {
                public int Id { get; set; }
                public string Content { get; set; }

                public int AuthorId { get; set; }

                public Guid TaskUid { get; set; }
        }
}