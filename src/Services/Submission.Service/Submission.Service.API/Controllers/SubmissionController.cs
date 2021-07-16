using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Submission.Service.API.Domain.Services;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Controllers
{
	[Route("/api/v1/submissions")]
	[ApiController]
	public class SubmissionController : ControllerBase
	{
		private readonly ISubmissionService _submissionService;
		private readonly ISubmissionDeadlinesService _submissionDeadlinesService;

		public SubmissionController(ISubmissionService submissionService, ISubmissionDeadlinesService submissionDeadlinesService)
		{
			_submissionService = submissionService;
			_submissionDeadlinesService = submissionDeadlinesService;
		}

		[HttpGet("deadlines/{workshopUid}")]
		public IActionResult GetSubmissionDeadlines(Guid workshopUid)
		{
			var deadlines = _submissionDeadlinesService.GetSubmissionDeadlines(workshopUid);

			if (deadlines == null)
				return NotFound();

			return Ok(deadlines);
		}

		[HttpGet("{workshopUid}")]
		[Authorize(Roles = "Instructor")]
		public async Task<IActionResult> GetSubmissions(Guid workshopUid)
		{
			var submissions = await _submissionService.GetSubmissionsByWorkshopUid(workshopUid);
			return Ok(submissions);
		}

		[HttpGet("{workshopUid}/author")]
		[Authorize(Roles = "Participant")]
		public async Task<IActionResult> GetSubmission(Guid workshopUid)
		{
			var userId = User.Identity.Name;

			// get student id from token with workshop Guid
			var submission = await _submissionService.GetAuthorSubmissionByWorkshopUidAsync(workshopUid, userId);

			if (submission == null)
				return NotFound();

			return Ok(submission);
		}

		[HttpPost("{workshopUid}")]
		[Authorize(Roles = "Participant, Instructor")]
		public async Task<IActionResult> CreateSubmission(Guid workshopUid, SaveSubmissionResource resource)
		{
			var authorId = User.Identity.Name;

			var result = await _submissionService.InsertAsync(workshopUid, resource, authorId);

			if (!result.Success)
				return BadRequest(result.Message);

			return Ok(result.Submission);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Participant, Instructor")]
		public async Task<IActionResult> UpdateSubmission(int id, SaveSubmissionResource resource)
		{
			var result = await _submissionService.UpdateAsync(id, resource);

			if (!result.Success)
				return NotFound(result.Message);

			return Ok(result.Submission);
		}
	}
}