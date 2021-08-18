using System.Collections.Generic;

namespace Review.Service.API.Resources
{
	public record ReviewSummaryResource
	{
		public string Participant { get; set; }
		public string ParticipantAuth0Id { get; set; }
		public string SubmissionContent { get; set; }
		public ReviewResource SelfReview { get; set; }
		public ReviewResource InstructorReview { get; set; }
		public IEnumerable<ReviewResource> PeerReviews { get; set; }
		public float Average { get; set; }
		public int MaxPointsSum { get; set; }
	}
}