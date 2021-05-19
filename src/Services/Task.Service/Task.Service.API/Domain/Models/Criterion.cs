using System;

namespace Task.Service.API.Domain.Models
{
        public class Criterion
        {
                public int Id { get; set; }
                public string Description { get; set; }
                public int MaxPoints { get; set; }
        }
}