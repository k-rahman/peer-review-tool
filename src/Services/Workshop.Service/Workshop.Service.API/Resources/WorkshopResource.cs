using System;
using System.Collections.Generic;

namespace Workshop.Service.API.Resources
{
	public record WorkshopResource
	{
		public int Id { get; set; }
		public Guid Uid { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTimeOffset SubmissionStart { get; set; }
		public DateTimeOffset SubmissionEnd { get; set; }
		public DateTimeOffset ReviewStart { get; set; }
		public DateTimeOffset ReviewEnd { get; set; }
		public DateTimeOffset Published { get; set; }
		public string Instructor { get; set; }
		public IEnumerable<CriterionResource> Criteria { get; set; }
	}
}