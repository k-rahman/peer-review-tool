using System.Collections.Generic;

namespace Review.Service.API.Resources
{
	public record ReviewsResource
	{
		public ReviewResource SelfReview { get; set; }
		public IEnumerable<ReviewResource> PeerReviews { get; set; }
	}
}