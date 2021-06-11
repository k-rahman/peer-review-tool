using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Workshop.Service.API.Domain.Models
{
        public class Participant
        {
                public int Id { get; set; }
                public string Auth0Id { get; set; }
                public string Email { get; set; }

                public ICollection<Workshop> Workshops = new List<Workshop>();

        }
}