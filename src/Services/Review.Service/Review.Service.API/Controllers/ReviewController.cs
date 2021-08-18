using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Review.Service.API.Domain.Services;
using Review.Service.API.Resources;

namespace Review.Service.API.Controller
{
	[Route("/api/v1/reviews")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IReviewService _reviewService;
		private readonly IReviewDeadlinesService _reviewDeadlinesService;
		private readonly IGradeService _gradeService;

		public ReviewController(
			IReviewService reviewService,
			IReviewDeadlinesService reviewDeadlinesService,
			IGradeService gradeService
			)
		{
			_reviewService = reviewService;
			_reviewDeadlinesService = reviewDeadlinesService;
			_gradeService = gradeService;

		}

		[HttpGet("deadlines/{workshopUid}")]
		public IActionResult GetReviewDeadlines(Guid workshopUid)
		{
			var deadlines = _reviewDeadlinesService.GetReviewDeadlines(workshopUid);

			if (deadlines == null)
				return NotFound();

			return Ok(deadlines);
		}

		[HttpGet("{workshopUid}")]
		[Authorize(Roles = "Participant")]
		public async Task<ActionResult> GetReviews(Guid workshopUid)
		{
			if (!User.IsInRole("Participant"))
				return Unauthorized();

			var userId = User.Identity.Name;
			var reviews = await _reviewService.GetParticipantReviewsByWorkshopUidAsync(workshopUid, userId);

			if (reviews == null)
				return NotFound();

			return Ok(reviews);

			// if (User.IsInRole("Instructor"))
			// 	var reviews = await _reviewService.GetInstructorReviewsSummaryByWorkshopUidAsync(workshopUid, userId);

			// else if (User.IsInRole("Participant"))

		}

		[HttpGet("summary/{workshopUid}")]
		[Authorize(Roles = "Participant, Instructor")]
		public async Task<ActionResult> GetReviewsSummary(Guid workshopUid)
		{
			var userId = User.Identity.Name;

			if (User.IsInRole("Instructor"))
			{
				var reviews = await _reviewService.GetInstructorReviewsSummaryByWorkshopUidAsync(workshopUid, userId);

				if (reviews == null)
					return NotFound();

				return Ok(reviews);
			}
			else if (User.IsInRole("Participant"))
			{
				var reviews = await _reviewService.GetParticipantReviewsSummaryByWorkshopUidAsync(workshopUid, userId);
				if (reviews == null)
					return NotFound();

				return Ok(reviews);
			}

			return Unauthorized();
		}

		[HttpGet("grades/{reviewId}")]
		[Authorize(Roles = "Participant, Instructor")]
		public async Task<ActionResult<GradesResource>> GetGrades(int reviewId)
		{
			var grades = await _gradeService.GetGradesByReviewId(reviewId);

			if (grades == null)
				return NotFound();

			return Ok(grades);
		}

		[HttpPut("{reviewId}")]
		public async Task<IActionResult> EditReview(int reviewId, SaveReviewResource grades)
		{
			var result = await _reviewService.UpdateReviewGrades(reviewId, grades);

			if (!result.Success)
				return NotFound();

			return Ok(result._review);
		}
	}
}