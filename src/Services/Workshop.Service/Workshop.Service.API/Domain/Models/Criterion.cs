using System;

namespace Workshop.Service.API.Domain.Models
{
        public class Criterion
        {
                public int Id { get; set; }
                public string Description { get; set; }
                public int MaxPoints { get; set; }

                public int WorkshopId { get; set; }
                public Domain.Models.Workshop Workshop { get; set; }
        }
}