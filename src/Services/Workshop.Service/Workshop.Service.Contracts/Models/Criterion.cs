namespace Workshop.Service.Contracts.Models
{
        public interface Criterion
        {
                int Id { get; }
                string Description { get; }
                int MaxPoints { get; }
        }
}