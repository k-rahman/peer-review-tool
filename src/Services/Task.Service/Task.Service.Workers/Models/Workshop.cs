using System;
using System.Collections.Generic;

namespace Task.Service.Workers.Models
{
        public class Workshop
        {
                public int id { get; set; }
                public Guid Uid { get; set; }
                public string InstructorId { get; set; }
                public ICollection<Participant> Participants { get; set; } = new List<Participant>();
        }
}