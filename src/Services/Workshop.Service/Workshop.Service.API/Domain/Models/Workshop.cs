using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Workshop.Service.API.Domain.Models
{
	public class Workshop
	{
		public int Id { get; set; }
		public Guid Uid { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int NumberOfReviews { get; set; }
		public DateTimeOffset SubmissionStart { get; set; }
		public DateTimeOffset SubmissionEnd { get; set; }
		public DateTimeOffset ReviewStart { get; set; }
		public DateTimeOffset ReviewEnd { get; set; }
		public DateTimeOffset Published { get; set; }
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset? Modified { get; set; }

		public string InstructorId { get; set; }
		public string Instructor { get; set; }

		public ICollection<Criterion> Criteria { get; } = new List<Criterion>();

		public ICollection<Participant> Participants { get; } = new List<Participant>();
	}
}