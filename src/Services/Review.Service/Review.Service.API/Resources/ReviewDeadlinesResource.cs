using System;

namespace Review.Service.API.Resources
{
	public class ReviewDeadlinesResource
	{
		public int Id { get; set; }
		public Guid Uid { get; set; }
		public DateTimeOffset ReviewStart { get; set; }
		public DateTimeOffset ReviewEnd { get; set; }
	}
}