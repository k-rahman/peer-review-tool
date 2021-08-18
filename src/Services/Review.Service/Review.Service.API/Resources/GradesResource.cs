using System.Collections.Generic;

namespace Review.Service.API.Resources
{
	public record GradesResource
	{
		public IEnumerable<GradeResource> Grades { get; set; }
	}
}