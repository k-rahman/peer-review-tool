using System.Collections.Generic;

namespace Review.Service.API.Resources
{
	public record ReviewsSummaryResource
	{
		public IEnumerable<ReviewSummaryResource> ReviewsSummary { get; set; }

	}
}