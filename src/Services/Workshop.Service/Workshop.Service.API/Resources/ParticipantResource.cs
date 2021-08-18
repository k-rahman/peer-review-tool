namespace Workshop.Service.API.Resources
{
	public record ParticipantResource
	{
		public string Auth0Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
	}
}