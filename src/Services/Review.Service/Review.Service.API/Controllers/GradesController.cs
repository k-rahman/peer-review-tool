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
	[Route("/api/v1/grades")]
	[ApiController]
	public class GradeController : ControllerBase
	{
		private readonly IGradeService _gradeService;

		public GradeController(IGradeService gradeService)
		{
			_gradeService = gradeService;

		}

		[HttpGet("{reviewId}")]
		[Authorize(Roles = "Participant, Instructor")]
		public async Task<ActionResult<GradesResource>> GetGrades(int reviewId)
		{
			var userId = User.Identity.Name;
			GradesResource grades = null;

			if (User.IsInRole("Participant"))
			{
				grades = await _gradeService.GetGradesByReviewId(reviewId);
			}

			if (grades == null)
				return NotFound();

			return Ok(grades);
		}

		// [HttpPost]
		// public async Task<IActionResult> SubmitGrade(SaveGradeResource grade)
		// {
		// 	return Ok();
		// }

		// [HttpPut("{reviewId}")]
		// public async Task<IActionResult> EditGrade(int reviewId, SaveGradeResource grades)
		// {
		// 	var result = await _reviewService.UpdateGradeGrades(reviewId, grades);

		// 	if (!result.Success)
		// 		return NotFound();

		// 	return Ok(result._review);
		// }
	}
}