using Review.Service.API.Resources;

namespace Review.Service.API.Domain.Services.Communication
{
	public class ReviewResponse : BaseResponse
	{
		public ReviewResource _review { get; private set; }

		private ReviewResponse(bool success, string message, ReviewResource review) : base(success, message)
		{
			_review = review;
		}

		/// <summary>
		/// Creates a success response.
		/// </summary>
		/// <param name="review">Saved review.</param>
		/// <returns>Response.</returns>
		public ReviewResponse(ReviewResource review) : this(true, string.Empty, review)
		{ }

		/// <summary>
		/// Creates am error response.
		/// </summary>
		/// <param name="message">Error message.</param>
		/// <returns>Response.</returns>
		public ReviewResponse(string message) : this(false, message, null)
		{ }
	}
}