namespace Workshop.Service.API.Resources
{
	public record SaveParticipantResource
	{
		public UserMetadataResource user_metadata { get; set; } // the naming convension follow auth0 convension
	}
}