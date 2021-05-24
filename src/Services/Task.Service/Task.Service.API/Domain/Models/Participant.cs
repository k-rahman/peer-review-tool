namespace Task.Service.API.Domain.Models
{
        public class Participant
        {
                public int Id { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }

                public int TaskId { get; set; }
                public Task Task { get; set; }
        }
}