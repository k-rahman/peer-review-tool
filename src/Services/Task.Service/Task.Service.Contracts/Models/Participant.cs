namespace Task.Service.Contracts.Models
{
        public interface Participant
        {
                string Auth0ID { get; }
                string Email { get; }
        }
}