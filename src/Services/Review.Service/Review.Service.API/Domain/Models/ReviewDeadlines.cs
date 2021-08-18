using System;
using System.Collections.Generic;

namespace Review.Service.API.Domain.Models
{
	public class ReviewDeadlines
	{
		public int Id { get; set; }
		public Guid Uid { get; set; }
		public DateTimeOffset ReviewStart { get; set; }
		public DateTimeOffset ReviewEnd { get; set; }
		public string InstructorId { get; set; }
	}
}