using System;
using System.Collections.Generic;

namespace Workshop.Service.API.Resources
{
	public record InstructorWorkshopResource : WorkshopResource
	{
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset? Modified { get; set; }
		public IEnumerable<ParticipantResource> Participants { get; set; } = new List<ParticipantResource>();
	}
}