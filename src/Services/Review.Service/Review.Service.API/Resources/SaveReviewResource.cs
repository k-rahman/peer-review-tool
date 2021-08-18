using System.Collections.Generic;

namespace Review.Service.API.Resources
{
	public record SaveReviewResource
	{
		public IEnumerable<SaveGradeResource> Grades { get; set; }
	}
}