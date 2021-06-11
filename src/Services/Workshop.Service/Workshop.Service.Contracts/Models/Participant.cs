namespace Workshop.Service.Contracts.Models
{
        public interface Participant
        {
                string Auth0Id { get; }
                string Email { get; }
        }
}