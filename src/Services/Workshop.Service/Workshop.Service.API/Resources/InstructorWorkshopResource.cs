using System;
using System.Collections.Generic;

namespace Workshop.Service.API.Resources
{
	public record InstructorWorkshopResource : WorkshopResource
	{
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset? Modified { get; set; }
		public IEnumerable<string> Participants { get; set; } = new List<string>();
	}
}